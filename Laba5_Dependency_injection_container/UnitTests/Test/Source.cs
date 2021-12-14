using System.Collections.Generic;
using DependencyInjectionContainer.Interfaces;
using DependencyInjectionContainer.Model;

namespace UnitTests.Test
{
    interface IMyClass
    {
        int TestCommand();
    }

    public class MyClass1 : IMyClass
    {
        public int TestCommand()
        {
            return 1;
        }
    }

    public class MyClass2 : IMyClass
    {
        public int TestCommand()
        {
            return 2;
        }
    }

    public interface ITestRepository { }

    public class TestRepository : ITestRepository { }

    public interface ITestClass
    {
        string SendMessage();
    }

    public class TestClass1 : ITestClass
    {
        public IEnumerable<ITestRepository> Repositories;

        public TestClass1(IEnumerable<ITestRepository> repositories)
        {
            Repositories = repositories;
        }

        public string SendMessage()
        {
            return "TestClass1";
        }
    }

    public class TestClass2 : ITestClass
    {
        public IEnumerable<ITestRepository> Repositories;

        public TestClass2(IEnumerable<ITestRepository> repositories)
        {
            Repositories = repositories;
        }

        public string SendMessage()
        {
            return "TestClass2";
        }
    }

    interface IFirstClass
    {
        ITestClass GetTestClass();
    }

    public class FirstClass : IFirstClass
    {
        public ITestClass TestClass;

        public FirstClass([DependencyKey(ServiceImplementations.First)] ITestClass testClass)
        {
            TestClass = testClass;
        }

        public ITestClass GetTestClass()
        {
            return TestClass;
        }
    }
}
