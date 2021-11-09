using AssemblyBrowserLibrary.Helper;
using AssemblyBrowserLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLibrary.Block.TypesData
{
    public class FieldData : GeneralDataType
    {
        public string FieldType { get; private set; }

        public FieldData(string name, string fieldType, string accessPermissions, Permissions accessPermissionsList)
            : base(name, accessPermissions, accessPermissionsList)
        {
            FieldType = fieldType;
            DataDeclaration = this.ToString();
        }

        protected override string ConvertPermissions()
        {
            string accessPermissions = "";

            if ((AccessPermissionsList & Permissions.Static) != 0)
            {
                accessPermissions = accessPermissions + "static";
            }
            if ((AccessPermissionsList & Permissions.Readonly) != 0)
            {
                accessPermissions = accessPermissions + "readonly";
            }

            return accessPermissions;
        }

        public override string ToString()
        {
            return $"{AccessPermissions} {ConvertPermissions()} {FieldType} {Name}";
        }
    }
}
