using System.Collections.Generic;
using AssemblyBrowserLibrary.Helper;
using AssemblyBrowserLibrary.Model;

namespace AssemblyBrowserLibrary.Block.TypesData
{
    public class DataType : GeneralDataType
    {
        public string TypeName { get; private set; }

        public List<GeneralDataType> Members { get; private set; }

        public bool IsExtension { get; private set; }

        public DataType(string typeName, List<GeneralDataType> members, bool isExtension,
            string name, string accessPermissions, Permissions accessPermissionsList)
            : base(name, accessPermissions, accessPermissionsList)
        {
            TypeName = typeName;
            Members = members;
            IsExtension = isExtension;
            DataDeclaration = this.ToString();
        }

        protected override string ConvertPermissions()
        {
            if ((AccessPermissionsList & Permissions.Abstract) != 0)
            {
                return "abstract";
            }
            if ((AccessPermissionsList & Permissions.Static) != 0)
            {
                return "static";
            }
            return "";
        }

        public override string ToString()
        {
            return $"{AccessPermissions} {ConvertPermissions()} {TypeName} {Name}";
        }
    }
}
