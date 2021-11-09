using AssemblyBrowserLibrary.Helper;
using AssemblyBrowserLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLibrary.Block.TypesData
{
    public class PropertyData : GeneralDataType
    {
        public string PropertyType { get; private set; }

        public MethodInfo[] Accessors { get; private set; }

        public PropertyData(string name, string accessPermissions, Permissions accessPermissionsList,
            string propertyType, MethodInfo[] accessors)
            : base(name, accessPermissions, accessPermissionsList)
        {
            PropertyType = propertyType;
            Accessors = accessors;
            DataDeclaration = this.ToString();
        }

        protected override string ConvertPermissions()
        {
            string accessPermissions = "";

            if ((AccessPermissionsList & Permissions.Sealed) != 0)
            {
                accessPermissions = accessPermissions + "sealed";
            }
            if ((AccessPermissionsList & Permissions.Abstract) != 0)
            {
                accessPermissions = accessPermissions + "abstract";
            }
            if ((AccessPermissionsList & Permissions.Virtual) != 0)
            {
                accessPermissions = accessPermissions + "virtual";
            }
            if ((AccessPermissionsList & Permissions.Static) != 0)
            {
                accessPermissions = accessPermissions + "static";
            }

            return accessPermissions;
        }

        public sealed override string ToString()
        {
            StringBuilder sb = new StringBuilder($"{AccessPermissions} {ConvertPermissions()} {PropertyType} {Name} {{");
            AddAccessorsString(sb);
            sb.Append(" } ");
            return sb.ToString();
        }

        private void AddAccessorsString(StringBuilder sb)
        {
            StringBuilder acc = new StringBuilder();
            foreach (var accessor in Accessors)
            {
                if (accessor.IsSpecialName)
                {
                    if (accessor.IsPrivate)
                    {
                        acc.Append("private ");
                    }
                    acc.Append($"{accessor.Name}, ");
                }
            }
            if (acc.Length > 0)
            {
                acc.Remove(acc.Length - 2, 2);
            }
            sb.Append(acc);
        }
    }
}
