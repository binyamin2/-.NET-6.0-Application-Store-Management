using System;
using System.Transactions;

namespace Targil0 // Note: actual namespace depends on the project name.
{
   partial class Program
    {
        static void Main(string[] args)
        {
            welcome5607();
            welcome5863();
            Console.ReadKey();
        }

        static partial void welcome5863();

        private static void welcome5607()
        {
            Console.Write("Enter yoyr name:");
            string user = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", user);
        }
    }
}