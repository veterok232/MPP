using AssemblyBrowserLibrary.Block.TypesData;
using AssemblyBrowserLibrary.Block.TypesHandlers;
using AssemblyBrowserLibrary.Helper;
using AssemblyBrowserLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLibrary.Block
{
    public class AssemblyBrowserHandler
    {
        private BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
            BindingFlags.Static | BindingFlags.FlattenHierarchy;

        public GeneralType FieldHandler { get; private set; }

        public GeneralType MethodHandler { get; private set; }

        public GeneralType PropertyHandler { get; private set; }

        public AssemblyBrowserHandler()
        {
            FieldHandler = new Field();
            MethodHandler = new Method();
            PropertyHandler = new Property();
        }

        public Type DataType { get; private set; }

        private string GetTypeName()
        {
            if (DataType.IsClass && DataType.BaseType.Name == "MulticastDelegate")
            { 
                return "delegate";
            }
            else if (DataType.IsClass)
            { 
                return "class"; 
            }
            else if (DataType.IsInterface)
            {
                return "interface";
            }
            else if (DataType.IsEnum)
            {
                return "enum";
            }
            else if (DataType.IsValueType && !DataType.IsPrimitive)
            { 
                return "struct";
            }

            return null;
        }

        private string GetAccessPermissions()
        {
            if (DataType.IsNotPublic)
            {
                return "internal";
            }

            return "public";
        }

        private Permissions GetAccessPermissionsList()
        {
            Permissions accessPermissions = 0;
            if (DataType.IsAbstract && DataType.IsSealed)
            {
                accessPermissions |= Permissions.Static;
            }
            else if (DataType.IsAbstract)
            {
                accessPermissions |= Permissions.Abstract;
            }
            return accessPermissions;
        }

        public DataType GetData(Type type)
        {
            DataType = type;
            var members = new List<GeneralDataType>();
            GetMethods(members);
            GetConstructors(members);
            GetFields(members);
            GetProperties(members);
            return new DataType(GetTypeName(), members, DataType.IsDefined(typeof(ExtensionAttribute)), DataType.Name,
                GetAccessPermissions(), GetAccessPermissionsList());
        }

        private void GetMethods(List<GeneralDataType> members)
        {
            foreach (var method in DataType.GetMethods(bindingFlags))
            {
                if (!method.IsSpecialName)
                {
                    members.Add(MethodHandler.GetData(method));
                }
            }
        }

        private void GetConstructors(List<GeneralDataType> members)
        {
            foreach (var constructor in DataType.GetConstructors(bindingFlags))
            {
                members.Add(MethodHandler.GetData(constructor));
            }
        }

        private void GetFields(List<GeneralDataType> members)
        {
            foreach (var field in DataType.GetFields(bindingFlags))
            {
                members.Add(FieldHandler.GetData(field));
            }
        }

        private void GetProperties(List<GeneralDataType> members)
        {
            foreach (var property in DataType.GetProperties(bindingFlags))
            {
                members.Add(PropertyHandler.GetData(property));
            }
        }
    }
}
