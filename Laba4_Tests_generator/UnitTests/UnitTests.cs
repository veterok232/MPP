using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestGeneratorLib.Block;
using TestGeneratorLib.Model;

namespace UnitTests
{
    [TestClass]
    public class UnitTests
    {
        private string Code = @"
            using System;
            using System.Collections.Generic;

            public class Foo
            {
                public IEnumerable<int> Intf { get; private set; }

                public Foo(IDisposable a, ICloneable b, int c, string d) { }

                public int TestFunction1(int e, int f)
                {
                    return 0;
                }

                public void TestFunction2(){}
            }

            public class Baz
            {
                public IEnumerable<string> Intf { get; private set; }

                public void TestFunction1() { }

                public void TestFunction2() { }
            }";

        private FileData Data;

        [TestInitialize]
        public void Init()
        {
            Data = CodeAnalyzer.GetFileData(Code);
        }

        [TestMethod]
        public void TestFileData()
        {
            Assert.IsNotNull(Data);
            Assert.AreEqual(2, Data.ClassesData.Count);
            
        }

        [TestMethod]
        public void TestClassesData()
        {
            Assert.IsNotNull(Data.ClassesData[0]);
            Assert.IsNotNull(Data.ClassesData[1]);
            Assert.AreEqual("Foo", Data.ClassesData[0].Name);
            Assert.AreEqual("Baz", Data.ClassesData[1].Name);
            Assert.AreEqual(1, Data.ClassesData[0].ConstructorsData.Count);
            Assert.AreEqual(2, Data.ClassesData[0].MethodsData.Count);
            Assert.AreEqual(0, Data.ClassesData[1].ConstructorsData.Count);
            Assert.AreEqual(2, Data.ClassesData[1].MethodsData.Count);
        }

        [TestMethod]
        public void TestConstructorsData()
        {
            Assert.IsNotNull(Data.ClassesData[0].ConstructorsData[0]);
            Assert.AreEqual("Foo", Data.ClassesData[0].ConstructorsData[0].Name);
            Assert.AreEqual(4, Data.ClassesData[0].ConstructorsData[0].Parameters.Count);
        }

        [TestMethod]
        public void TestMethodsData()
        {
            Assert.IsNotNull(Data.ClassesData[0].MethodsData[0]);
            Assert.AreEqual("TestFunction1", Data.ClassesData[0].MethodsData[0].Name);
            Assert.AreEqual(2, Data.ClassesData[0].MethodsData[0].Parameters.Count);
            Assert.AreEqual("int", Data.ClassesData[0].MethodsData[0].ReturnType);

            Assert.IsNotNull(Data.ClassesData[0].MethodsData[1]);
            Assert.AreEqual("TestFunction2", Data.ClassesData[0].MethodsData[1].Name);
            Assert.AreEqual(0, Data.ClassesData[0].MethodsData[1].Parameters.Count);
            Assert.AreEqual("void", Data.ClassesData[0].MethodsData[1].ReturnType);

            Assert.IsNotNull(Data.ClassesData[1].MethodsData[0]);
            Assert.AreEqual("TestFunction1", Data.ClassesData[1].MethodsData[0].Name);
            Assert.AreEqual(0, Data.ClassesData[1].MethodsData[0].Parameters.Count);
            Assert.AreEqual("void", Data.ClassesData[1].MethodsData[0].ReturnType);

            Assert.IsNotNull(Data.ClassesData[1].MethodsData[1]);
            Assert.AreEqual("TestFunction2", Data.ClassesData[1].MethodsData[1].Name);
            Assert.AreEqual(0, Data.ClassesData[1].MethodsData[1].Parameters.Count);
            Assert.AreEqual("void", Data.ClassesData[1].MethodsData[1].ReturnType);
        }
    }
}
