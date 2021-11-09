using AssemblyBrowserLibrary.Helper;
using AssemblyBrowserLibrary.Model;
using System;
using System.Reflection;

namespace AssemblyBrowserLibrary.Block.TypesHandlers
{
    public abstract class GeneralType
    {
        protected abstract string GetAccessPermissions();

        protected abstract Permissions GetAccessPermissionsList();

        public abstract GeneralDataType GetData(MemberInfo data);

        protected string ConvertTypeName(Type type)
        {
            if (type.IsGenericType)
            {
                var genericArgsString = "";
                var genericArguments = type.GetGenericArguments();
                var result = type.Name.Remove(type.Name.Length - 2, 2) + "<";
                foreach (var genericArgument in genericArguments)
                {
                    genericArgsString += ConvertTypeName(genericArgument) + ", ";
                }
                genericArgsString = genericArgsString.Length > 0 ? genericArgsString.Remove(genericArgsString.Length - 2, 2) :
                    genericArgsString;
                return result + genericArgsString + ">";
            }
            else
            {
                return type.Name;
            }
        }
    }
}
