using System;
using System.Transactions;

namespace Targil0 // Note: actual namespace depends on the project name.
{
   partial class Program
    {
        static void Main(string[] args)
        {
            Welcome5607();
            Welcome5863();
            Console.ReadKey();
        }

        static partial void Welcome5863();

        private static void Welcome5607()
        {
            Console.Write("Enter yoyr name:");
            string user = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", user);
        }
    }
}