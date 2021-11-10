using AssemblyBrowserLibrary.Block.TypesData;
using AssemblyBrowserLibrary.Helper;
using AssemblyBrowserLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLibrary.Block.TypesHandlers
{
    public class Field : GeneralType
    {
        public FieldInfo FieldInfo { get; private set; }

        protected override string GetAccessPermissions()
        {
            if (FieldInfo.IsPrivate)
            {
                return "private";
            }
            else if (FieldInfo.IsPublic)
            {
                return "public";
            }
            else if (FieldInfo.IsAssembly)
            {
                return "internal";
            }
            else if (FieldInfo.IsFamilyAndAssembly)
            {
                return "private protected";
            }
            return "protected internal";
        }

        protected override Permissions GetAccessPermissionsList()
        {
            Permissions accessPermissions = 0;
            if (FieldInfo.IsStatic)
            {
                accessPermissions |= Permissions.Static;
            }
            else if (FieldInfo.IsInitOnly)
            {
                accessPermissions |= Permissions.Readonly;
            }
            return accessPermissions;
        }

        public override GeneralDataType GetData(MemberInfo data)
        {
            FieldInfo = (FieldInfo)data;
            return new FieldData(
                FieldInfo.Name,
                ConvertTypeName(FieldInfo.FieldType),
                GetAccessPermissions(),
                GetAccessPermissionsList());
        }
    }
}
