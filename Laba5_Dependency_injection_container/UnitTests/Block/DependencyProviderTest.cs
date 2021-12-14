using NUnit.Framework;
using UnitTests.Test;
using DependencyInjectionContainer.Interfaces;
using DependencyInjectionContainer.Model;
using DependencyInjectionContainer.Block;

namespace UnitTests.Block
{
    [TestFixture]
    public class DependenciesProviderTest
    {
        private DependenciesConfiguration Configuration;

        [SetUp]
        public void Init()
        {
            Configuration = new DependenciesConfiguration();
        }

        [Test]
        public void TwoImplementationsWithoutNestedInterfaceTest()
        {
            Configuration.Register<IMyClass, MyClass1>(TTL.Singleton, ServiceImplementations.First);
            Configuration.Register<IMyClass, MyClass2>(TTL.InstancePerDependency, ServiceImplementations.Second);

            var provider = new DependencyProvider(Configuration);

            int expected = 1;
            var actual = provider.Resolve<IMyClass>(ServiceImplementations.First);

            Assert.AreEqual(expected, actual.TestCommand());

            expected = 2;
            actual = provider.Resolve<IMyClass>(ServiceImplementations.Second);

            Assert.AreEqual(expected, actual.TestCommand());
        }

        [Test]
        public void TwoImplementationsWithNestedInterfaceTest()
        {
            Configuration.Register<ITestRepository, TestRepository>(TTL.InstancePerDependency);
            Configuration.Register<ITestClass, TestClass1>(TTL.Singleton, ServiceImplementations.First);
            Configuration.Register<ITestClass, TestClass2>(TTL.InstancePerDependency, ServiceImplementations.Second);

            var provider = new DependencyProvider(Configuration);

            string expected = "TestClass1";
            var actual = provider.Resolve<ITestClass>(ServiceImplementations.First);

            Assert.AreEqual(expected, actual.SendMessage());

            expected = "TestClass2";
            actual = provider.Resolve<ITestClass>(ServiceImplementations.Second);

            Assert.AreEqual(expected, actual.SendMessage());
        }

        [Test]
        public void OneImplementationWithCustomAttribute()
        {
            Configuration.Register<ITestClass, TestClass1>(TTL.Singleton, ServiceImplementations.First);
            Configuration.Register<ITestClass, TestClass2>(TTL.InstancePerDependency, ServiceImplementations.Second);
            Configuration.Register<ITestRepository, TestRepository>(TTL.Singleton);
            Configuration.Register<IFirstClass, FirstClass>(TTL.InstancePerDependency);

            var provider = new DependencyProvider(Configuration);

            string expected = "UnitTests.Test.TestClass1";
            var actual = provider.Resolve<IFirstClass>();

            Assert.IsInstanceOf(typeof(TestClass1), actual.GetTestClass());
            Assert.AreEqual(expected, actual.GetTestClass().ToString());
        }
    }
}
