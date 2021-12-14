using System;
using System.Collections.Generic;
using NUnit.Framework;
using Newtonsoft.Json;
using UnitTests.Test;
using DependencyInjectionContainer.Interfaces;
using DependencyInjectionContainer.Block;
using DependencyInjectionContainer.Model;

namespace UnitTests.Configuration
{
    [TestFixture]
    public class DependenciesConfigurationTest
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
            var expected = new Dictionary<Type, List<Implementation>>()
            {
                {
                    typeof(IMyClass),
                    new List<Implementation>()
                    {
                        new Implementation(typeof(MyClass1), TTL.Singleton, ServiceImplementations.None),
                        new Implementation(typeof(MyClass2), TTL.InstancePerDependency, ServiceImplementations.None)
                    }
                }
            };

            Configuration.Register<IMyClass, MyClass1>(TTL.Singleton);
            Configuration.Register<IMyClass, MyClass2>(TTL.InstancePerDependency);

            var expectedJson = JsonConvert.SerializeObject(Configuration.Dependencies);
            var resultJson = JsonConvert.SerializeObject(expected);

            Assert.AreEqual(expectedJson, resultJson);
        }

        [Test]
        public void TwoImplementationsWithNestedInterfaceTest()
        {
            var expected = new Dictionary<Type, List<Implementation>>()
            {
                {
                    typeof(ITestClass),
                    new List<Implementation>()
                    {
                        new Implementation(typeof(TestClass1), TTL.Singleton, ServiceImplementations.None),
                        new Implementation(typeof(TestClass2), TTL.InstancePerDependency, ServiceImplementations.None)
                    }
                }
            };

            Configuration.Register<ITestClass, TestClass1>(TTL.Singleton);
            Configuration.Register<ITestClass, TestClass2>(TTL.InstancePerDependency);

            var expectedJson = JsonConvert.SerializeObject(Configuration.Dependencies);
            var resultJson = JsonConvert.SerializeObject(expected);

            Assert.AreEqual(expectedJson, resultJson);
        }

        [Test]
        public void OneImplementationWithCustomAttribute()
        {
            var expected = new Dictionary<Type, List<Implementation>>()
            {
                {
                    typeof(IFirstClass),
                    new List<Implementation>()
                    {
                        new Implementation(typeof(FirstClass), TTL.InstancePerDependency, ServiceImplementations.None)
                    }
                }
            };

            Configuration.Register<IFirstClass, FirstClass>(TTL.InstancePerDependency);

            var expectedJson = JsonConvert.SerializeObject(Configuration.Dependencies);
            var resultJson = JsonConvert.SerializeObject(expected);

            Assert.AreEqual(expectedJson, resultJson);
        }
    }
}
