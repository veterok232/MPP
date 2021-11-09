using System;

namespace AssemblyBrowserLibrary.Helper
{
    [Flags]
    public enum Permissions
    {
        Abstract = 1,
        Virtual = 2,
        Sealed = 4,
        Static = 8,
        Readonly = 16
    }
}
