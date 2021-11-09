using AssemblyBrowserLibrary.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowserLibrary.Model
{
    public abstract class GeneralDataType
    {
        public string Name { get; private set; }

        public string AccessPermissions { get; private set; }

        public Permissions AccessPermissionsList { get; private set; }

        public string DataDeclaration { get; protected set; }

        protected GeneralDataType(string name, string accessPermissions, Permissions accessPermissionsList)
        {
            Name = name;
            AccessPermissions = accessPermissions;
            AccessPermissionsList = accessPermissionsList;
        }

        protected abstract string ConvertPermissions();
    }
}
