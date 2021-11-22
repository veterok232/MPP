using System;
using TestGeneratorMain.Block;

namespace TestGeneratorMain
{
    /// <summary>
    ///     Entry point
    /// </summary>
    class Program
    {
        /// <summary>
        ///     Main method
        /// </summary>
        /// 
        /// <param name="args">Arguments of command line</param>
        static void Main(string[] args)
        {
            var src = @"..\..\Tests\Source";
            var files = new string[] { "Foo.cs", "Bar.cs"};
            var dest = @"..\..\Tests\Result"; 
            
            new Pipeline().Generate(src, files, dest, 2);
            Console.ReadLine();
        }
    }
}
