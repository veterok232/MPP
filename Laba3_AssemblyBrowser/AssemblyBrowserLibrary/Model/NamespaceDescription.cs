using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyBrowserLibrary.Block;
using AssemblyBrowserLibrary.Block.TypesData;

namespace AssemblyBrowserLibrary.Model
{
    public class NamespaceDescription
    {
        public string Name { get; private set; }

        public List<DataType> Types { get; private set; }

        public NamespaceDescription(string name, List<DataType> types)
        {
            Name = name;
            Types = types;
        }
    }
}
