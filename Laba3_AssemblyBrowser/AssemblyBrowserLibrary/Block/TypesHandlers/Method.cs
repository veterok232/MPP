using AssemblyBrowserLibrary.Block.TypesData;
using AssemblyBrowserLibrary.Helper;
using AssemblyBrowserLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLibrary.Block.TypesHandlers
{
    public class Method : GeneralType
    {
        public MethodBase MethodInfo { get; private set; }

        public override GeneralDataType GetData(MemberInfo data)
        {
            MethodInfo = (MethodBase)data;

            var isExtension = false;
            string returnType = string.Empty;

            if (data is MethodInfo)
            {
                var method = ((MethodInfo)MethodInfo);
                returnType = ConvertTypeName(method.ReturnType);
                isExtension = (method.GetBaseDefinition().DeclaringType == method.DeclaringType) &&
                    MethodInfo.IsDefined(typeof(ExtensionAttribute));
            }

            return new MethodData(MethodInfo.Name, GetAccessPermissions(), GetAccessPermissionsList(),
                returnType, GetParameters(), isExtension);
        }

        protected override string GetAccessPermissions()
        {
            if (MethodInfo.IsPrivate)
            {
                return "private";
            }
            if (MethodInfo.IsPublic)
            {
                return "public";
            }
            if (MethodInfo.IsAssembly)
            {
                return "internal";
            }
            if (MethodInfo.IsFamilyAndAssembly)
            {
                return "private protected";
            }

            return "protected internal";
        }

        protected override Permissions GetAccessPermissionsList()
        {
            Permissions accessPermissions = 0;

            if (MethodInfo.IsAbstract)
            {
                accessPermissions |= Permissions.Abstract;
            }
            if (MethodInfo.IsVirtual)
            {
                accessPermissions |= Permissions.Virtual;
            }
            if (MethodInfo.IsStatic)
            {
                accessPermissions |= Permissions.Static;
            }

            return accessPermissions;
        }

        private Dictionary<string, string> GetParameters()
        {
            var parameters = new Dictionary<string, string>();

            try
            {
                foreach (ParameterInfo parameterInfo in MethodInfo.GetParameters())
                {
                    parameters.Add(parameterInfo.Name, ConvertTypeName(parameterInfo.ParameterType));
                }

                return parameters;
            }
            catch
            {
                return new Dictionary<string, string>();
            }
        }
    }
}
