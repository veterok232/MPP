using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakerLib.Interfaces;
using FakerLib;
using FakerLib.Generators.CustomGenerators;
using FakerApp.TestClasses;

namespace FakerApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            FakerConfig fakerConfig = new FakerConfig();
            fakerConfig.Add<Foo, string, PersonNameGenerator>(obj => obj.StrA);

            Faker faker = new Faker(fakerConfig);

            int testInt = faker.Create<int>();
            char testChar = faker.Create<char>();
            DateTime dateTime = faker.Create<DateTime>();
            string testString = faker.Create<string>();
            List<char> testList = faker.Create<List<char>>();
            Foo foo = faker.Create<Foo>();
            Bar bar = faker.Create<Bar>();
            C c = faker.Create<C>();

            Console.WriteLine("TestInt: " + testInt);
            Console.WriteLine("TestChar: " + testChar);
            Console.WriteLine("TestDateTime: " + dateTime.ToString());
            Console.WriteLine("TestString: " + testString);

            Console.WriteLine();

            foreach (var item in testList)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine();

            Console.WriteLine(foo.ToString());
            Console.WriteLine();
            Console.WriteLine(bar.ToString());
            Console.WriteLine();
            Console.WriteLine(c.ToString());
        }
    }
}
