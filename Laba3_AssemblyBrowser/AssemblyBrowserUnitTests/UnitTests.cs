using AssemblyBrowserLibrary;
using AssemblyBrowserLibrary.Model;
using NUnit.Framework;
using System.Collections.Generic;

namespace AssemblyBrowserUnitTests
{
    [TestFixture]
    public class Tests
    {
        private List<NamespaceDescription> assemblyInfo;
        private List<NamespaceDescription> assemblyInfoExtensions;

        private readonly string pathToAssembly = @"D:\Studying\Univer\5_semester\ÑÏÏ\Laba2_Faker\FakerLib\bin\Debug\Faker.dll";
        private readonly string pathToAssemblyExtensions = @"D:\Studying\Univer\5_semester\ÑÏÏ\Test\bin\Debug\Test.dll";

        [SetUp]
        public void Setup()
        {
            var assemblyBrowser = new AssemblyBrowser();
            assemblyInfo = assemblyBrowser.GetAssemblyData(pathToAssembly);
            assemblyInfoExtensions = assemblyBrowser.GetAssemblyData(pathToAssemblyExtensions);
        }

        [Test]
        public void TestNamespacesCount()
        {
            Assert.AreEqual(8, assemblyInfo.Count);
        }

        [Test]
        public void TestNamespacesNames()
        {
            Assert.IsTrue(assemblyInfo[0].Name.Equals("FakerLib"));
            Assert.IsTrue(assemblyInfo[2].Name.Equals("FakerLib.Interfaces"));
            Assert.IsTrue(assemblyInfo[3].Name.Equals("FakerLib.Generators.SystemTypesGenerators.ValueTypesGenerators"));
        }

        [Test]
        public void TestTypesWithoutNamespaces()
        {
            Assert.AreEqual(1, assemblyInfoExtensions[0].Types.Count);
            Assert.IsNotNull(assemblyInfoExtensions[0].Types[0]);
            Assert.IsTrue(assemblyInfoExtensions[0].Name.Equals("Without namespace"));
            Assert.IsTrue(assemblyInfoExtensions[0].Types[0].Name.Equals("Class2"));
        }

        [Test]
        public void TestTypesWithExtensionMethods()
        {
            Assert.NotNull(assemblyInfoExtensions[1].Types);
            Assert.IsTrue(assemblyInfoExtensions[1].Types[0].Name.Equals("Class1"));
            Assert.AreEqual(11, assemblyInfoExtensions[1].Types[0].Members.Count);
            Assert.IsTrue(assemblyInfoExtensions[1].Types[0].Members[10].Name.Equals("ExtensionMethod"));
            Assert.IsTrue(assemblyInfoExtensions[1].Types[0].Members[10].ToString().Equals("public static  String ExtensionMethod(Class1 obj, String message, Int32 count)(extension method)"));
        }

        [Test]
        public void TestFakerLibInterfacesTypes()
        {
            Assert.AreEqual(2, assemblyInfo[2].Types.Count);
            Assert.IsTrue(assemblyInfo[2].Types[0].Name.Equals("IFaker"));
            Assert.IsTrue(assemblyInfo[2].Types[1].Name.Equals("IFakerConfig"));
            Assert.IsTrue(assemblyInfo[2].Types[0].Members[0].ToString().Equals("public virtual abstract  T Create()"));
        }

        [Test]
        public void TestFakerLibReferenceTypesGenerators()
        {
            Assert.AreEqual(3, assemblyInfo[4].Types.Count);
            Assert.IsTrue(assemblyInfo[4].Types[0].Name.Equals("ListGenerator"));
            Assert.IsTrue(assemblyInfo[4].Types[1].Name.Equals("StringGenerator"));
            Assert.IsTrue(assemblyInfo[4].Types[2].Name.Equals("<>c__DisplayClass3_0"));
            Assert.AreEqual(11, assemblyInfo[4].Types[2].Members.Count);
            Assert.IsTrue(assemblyInfo[4].Types[2].Members[0].Name.Equals("<FakerLib.Generators.Interfaces.IGenerator.Generate>b__0"));
        }
    }
}