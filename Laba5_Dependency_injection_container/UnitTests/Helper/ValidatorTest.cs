using NUnit.Framework;
using UnitTests.Test;
using DependencyInjectionContainer.Interfaces;
using DependencyInjectionContainer.Model;
using DependencyInjectionContainer.Block;
using DependencyInjectionContainer.Helper;

namespace UnitTests.Helper
{
    [TestFixture]
    public class ValidatorTest
    {
        private DependenciesConfiguration Configuration;

        [SetUp]
        public void Init()
        {
            Configuration = new DependenciesConfiguration();
        }

        [Test]
        public void TwoImplementationsWithoutNestedInterfaceValidTest()
        {
            Configuration.Register<IMyClass, MyClass1>(TTL.Singleton);
            Configuration.Register<IMyClass, MyClass2>(TTL.InstancePerDependency);

            var validator = new Validator(Configuration);

            Assert.IsTrue(validator.IsValid());
        }

        [Test]
        public void TwoImplementationsWithNestedInterfaceValidTest()
        {
            Configuration.Register<ITestRepository, TestRepository>(TTL.InstancePerDependency);
            Configuration.Register<ITestClass, TestClass1>(TTL.Singleton);
            Configuration.Register<ITestClass, TestClass2>(TTL.InstancePerDependency);

            var validator = new Validator(Configuration);

            Assert.IsTrue(validator.IsValid());
        }

        [Test]
        public void TwoImplementationsWithNestedInterfaceInValidTest()
        {
            Configuration.Register<ITestClass, TestClass1>(TTL.Singleton);
            Configuration.Register<ITestClass, TestClass2>(TTL.InstancePerDependency);

            var validator = new Validator(Configuration);

            Assert.IsFalse(validator.IsValid());
        }
    }
}
