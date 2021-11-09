using AssemblyBrowserLibrary.Block.TypesData;
using AssemblyBrowserLibrary.Helper;
using AssemblyBrowserLibrary.Model;
using System.Reflection;

namespace AssemblyBrowserLibrary.Block.TypesHandlers
{
    public class Property : GeneralType
    {
        public PropertyInfo PropertyInfo { get; private set; }

        public override GeneralDataType GetData(MemberInfo data)
        {
            PropertyInfo = (PropertyInfo)data;

            return new PropertyData(PropertyInfo.Name, GetAccessPermissions(), GetAccessPermissionsList(),
                ConvertTypeName(PropertyInfo.PropertyType), PropertyInfo.GetAccessors(true));
        }

        protected override string GetAccessPermissions()
        {
            var accessor = PropertyInfo.GetAccessors(true)[0];

            if (accessor.IsPrivate)
            {
                return "private";
            }
            if (accessor.IsPublic)
            {
                return "public";
            }
            if (accessor.IsAssembly)
            {
                return "internal";
            }
            if (accessor.IsFamilyAndAssembly)
            {
                return "private protected";
            }

            return "protected internal";
        }

        protected override Permissions GetAccessPermissionsList()
        {
            Permissions accessPermissions = 0;
            var accessor = PropertyInfo.GetAccessors(true)[0];

            if (accessor.IsAbstract)
            {
                accessPermissions |= Permissions.Abstract;
            }
            if (accessor.IsVirtual)
            {
                accessPermissions |= Permissions.Virtual;
            }
            if (accessor.IsStatic)
            {
                accessPermissions |= Permissions.Static;
            }

            return accessPermissions;
        }
    }
}
