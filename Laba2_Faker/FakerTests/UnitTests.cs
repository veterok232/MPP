using System;
using System.Collections.Generic;
using NUnit.Framework;
using FakerLib;
using FakerLib.Interfaces;
using FakerLib.Service;
using FakerLib.Generators.CustomGenerators;
using FakerApp.TestClasses;

namespace FakerTests
{
    /// <summary>
    /// Unit tests for Faker project
    /// </summary>
    public class UnitTests
    {
        private Foo fooDTO;
        private C cDTO;

        [SetUp]
        public void Setup()
        {
            FakerConfig fakerConfig = new FakerConfig();
            fakerConfig.Add<Foo, string, PersonNameGenerator>(obj => obj.StrA);

            Faker faker = new Faker(fakerConfig);

            fooDTO = faker.Create<Foo>();
            cDTO = faker.Create<C>();
        }

        [Test]
        public void FooDTOTest()
        {
            Assert.NotNull(fooDTO);

            Assert.NotNull(fooDTO.GetStrB());
            Assert.GreaterOrEqual(fooDTO.GetStrB().Length, 1);

            Assert.NotNull(fooDTO.DecimalA);
            Assert.IsInstanceOf(typeof(decimal), fooDTO.DecimalA);

            Assert.NotNull(fooDTO.DoubleA);
            Assert.IsInstanceOf(typeof(double), fooDTO.DoubleA);

            Assert.NotNull(fooDTO.FloatA);
            Assert.IsInstanceOf(typeof(float), fooDTO.FloatA);

            Assert.NotNull(fooDTO.IntA);
            Assert.IsInstanceOf(typeof(int), fooDTO.IntA);

            Assert.NotNull(fooDTO.StrA);
            Assert.GreaterOrEqual(fooDTO.StrA.Length, 1);

            Assert.NotNull(fooDTO.Bar);
            Assert.NotNull(fooDTO.DoubleList);
        }

        [Test]
        public void NestedInFoo_BarDTOTest()
        {
            Assert.NotNull(fooDTO.Bar);

            Assert.NotNull(fooDTO.Bar.BoolA);
            Assert.IsInstanceOf(typeof(bool), fooDTO.Bar.BoolA);

            Assert.NotNull(fooDTO.Bar.ByteA);
            Assert.IsInstanceOf(typeof(byte), fooDTO.Bar.ByteA);

            Assert.NotNull(fooDTO.Bar.CharA);
            Assert.IsInstanceOf(typeof(char), fooDTO.Bar.CharA);

            Assert.NotNull(fooDTO.Bar.c);
            Assert.IsNull(fooDTO.Bar.c.Bar);
            Assert.IsNull(fooDTO.Bar.c.Foo);

            Assert.IsNull(fooDTO.Bar.collection);

            Assert.NotNull(fooDTO.Bar.DateTimeA);
            Assert.IsInstanceOf(typeof(DateTime), fooDTO.Bar.DateTimeA);
        }

        [Test]
        public void ListInFooTest()
        {
            Assert.NotNull(fooDTO.DoubleList);
            Assert.IsInstanceOf(typeof(List<double>), fooDTO.DoubleList);
            Assert.GreaterOrEqual(fooDTO.DoubleList.Count, 1);

            foreach (var listItem in fooDTO.DoubleList)
            {
                Assert.NotNull(listItem);
                Assert.IsInstanceOf(typeof(double), listItem);
            }
        }

        [Test]
        public void CDTOTest()
        {
            Assert.NotNull(cDTO);

            Assert.NotNull(cDTO.GetUShortA());
            Assert.IsInstanceOf(typeof(ushort), cDTO.GetUShortA());

            Assert.NotNull(cDTO.SByteA);
            Assert.IsInstanceOf(typeof(sbyte), cDTO.SByteA);

            Assert.NotNull(cDTO.ShortA);
            Assert.IsInstanceOf(typeof(short), cDTO.ShortA);

            Assert.NotNull(cDTO.UIntA);
            Assert.IsInstanceOf(typeof(uint), cDTO.UIntA);

            Assert.NotNull(cDTO.ULongA);
            Assert.IsInstanceOf(typeof(ulong), cDTO.ULongA);

            Assert.NotNull(cDTO.Foo);
            Assert.NotNull(cDTO.Bar);
            Assert.NotNull(cDTO.Foo.Bar);

            Assert.IsNull(cDTO.Bar.c);

            Assert.NotNull(cDTO.Foo.DoubleList);
            Assert.GreaterOrEqual(cDTO.Foo.DoubleList.Count, 1);
        }
    }
}