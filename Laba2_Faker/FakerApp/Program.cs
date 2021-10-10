using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakerLib.Interfaces;
using FakerLib;

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

            Console.WriteLine(testInt);
            Console.WriteLine(testChar);
            Console.WriteLine(dateTime.ToString());
            Console.WriteLine(testString);
        }
    }
}
