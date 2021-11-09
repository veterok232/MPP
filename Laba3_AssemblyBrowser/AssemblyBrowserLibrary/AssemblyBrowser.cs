using AssemblyBrowserLibrary.Block;
using AssemblyBrowserLibrary.Block.TypesData;
using AssemblyBrowserLibrary.Model;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AssemblyBrowserLibrary
{
    public class AssemblyBrowser
    {
        public AssemblyBrowserHandler TypeHandler { get; private set; }

        public Dictionary<string, List<DataType>> NamespacesDictionary { get; private set; }

        public AssemblyBrowser()
        {
            TypeHandler = new AssemblyBrowserHandler();
            NamespacesDictionary = new Dictionary<string, List<DataType>>();
        }

        public bool TryGetExtensionMethods(DataType type, out List<MethodData> methods, out List<int> indexes)
        {
            methods = new List<MethodData>();
            indexes = new List<int>();

            for (int j = 0; j < type.Members.Count; j++)
            {
                var member = type.Members[j];
                if (!(member is MethodData) || !((MethodData)member).IsExtension)
                {
                    continue;
                }
                methods.Add((MethodData)member);
                indexes.Add(j);
            }

            return methods.Count > 0 ? true : false;
        }

        public DataType FindExtensibleType(string extensibleType)
        {
            foreach (var keyValue in NamespacesDictionary)
            {
                var types = keyValue.Value;
                foreach (var type in keyValue.Value)
                {
                    if (type.Name == extensibleType)
                    {
                        return type;
                    }
                }
            }

            return null;
        }

        public void ProcessExtensionMethods()
        {
            foreach (var keyValue in NamespacesDictionary)
            {
                var types = keyValue.Value;
                for (int i = 0; i < types.Count; i++)
                {
                    var type = types[i];
                    if (!type.IsExtension)
                    {
                        continue;
                    }
                    if (TryGetExtensionMethods(type, out List<MethodData> methods, out _))
                    {
                        type.Members.RemoveAll(elem => methods.Any(newElem => elem == newElem));
                        foreach (var method in methods)
                        {
                            var extensibleType = FindExtensibleType(method.Parameters.Values.First());
                            extensibleType?.Members.Add(method);
                        }
                    }
                }
            }
        }

        public List<NamespaceDescription> GetAssemblyData(string path)
        {
            NamespacesDictionary.Clear();
            var assembly = Assembly.LoadFrom(path);
            var assemblyTypes = assembly.GetTypes();
            foreach (var assemblyType in assemblyTypes)
            {
                var typeData = TypeHandler.GetData(assemblyType);
                if (!NamespacesDictionary.TryGetValue(assemblyType?.Namespace ?? "Without namespace", out _))
                {
                    NamespacesDictionary.Add(assemblyType?.Namespace ?? "Without namespace", new List<DataType>() { typeData });
                }
            }
            ProcessExtensionMethods();
            var namespaces = new List<NamespaceDescription>();
            foreach (var pair in NamespacesDictionary)
            {
                namespaces.Add(new NamespaceDescription(pair.Key, pair.Value));
            }
            return namespaces;
        }
    }
}
