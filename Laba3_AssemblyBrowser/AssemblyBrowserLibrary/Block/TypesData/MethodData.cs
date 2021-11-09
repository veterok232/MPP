using AssemblyBrowserLibrary.Helper;
using AssemblyBrowserLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLibrary.Block.TypesData
{ 
    public class MethodData : GeneralDataType
    {
        public string ReturnType { get; private set; }

        public bool IsExtension { get; private set; }

        public Dictionary<string, string> Parameters { get; private set; }

        public MethodData(string name, string accessPermissions, Permissions accessPermissionsList,
            string returnType, Dictionary<string, string> parameters, bool isExtension)
            : base(name, accessPermissions, accessPermissionsList)
        {
            ReturnType = returnType;
            Parameters = parameters;
            IsExtension = isExtension;
            DataDeclaration = this.ToString();
        }

        protected override string ConvertPermissions()
        {
            string permissionModifyiers = "";

            if ((AccessPermissionsList & Permissions.Static) != 0)
            {
                permissionModifyiers = permissionModifyiers + "static";
            }
            if ((AccessPermissionsList & Permissions.Virtual) != 0)
            {
                permissionModifyiers = permissionModifyiers + "virtual";
            }
            if ((AccessPermissionsList & Permissions.Sealed) != 0)
            {
                permissionModifyiers = permissionModifyiers + "sealed";
            }
            if ((AccessPermissionsList & Permissions.Abstract) != 0)
            {
                permissionModifyiers = permissionModifyiers + "abstract";
            }

            return permissionModifyiers;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{AccessPermissions} {ConvertPermissions()} {ReturnType} {Name}(");
            AddParametersString(sb);
            if (IsExtension)
            {
                sb.Append("(extension method)");
            }
            return sb.ToString();
        }

        private void AddParametersString(StringBuilder sb)
        {
            foreach (var parameter in Parameters)
            {
                sb.Append($"{parameter.Key} {parameter.Value}, ");
            }
            if (Parameters.Count > 0)
            {
                sb.Remove(sb.Length - 2, 2);
            }
            sb.Append(")");
        }
    }
}
