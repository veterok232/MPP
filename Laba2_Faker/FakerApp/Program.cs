using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakerLib.Interfaces;
using FakerLib;
using FakerApp.TestClasses;

namespace FakerApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Faker faker = new Faker();

            int testInt = faker.Create<int>();
            char testChar = faker.Create<char>();
            DateTime dateTime = faker.Create<DateTime>();
            string testString = faker.Create<string>();
            List<char> testList = faker.Create<List<char>>();
            Foo foo = faker.Create<Foo>();

            Console.WriteLine(testInt);
            Console.WriteLine(testChar);
            Console.WriteLine(dateTime.ToString());
            Console.WriteLine(testString);

            foreach (var item in testList)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine(foo.ToString());
        }
    }
}
