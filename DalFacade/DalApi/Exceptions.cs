using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

public class NotFoundException : Exception
{
    //Overriding the Message property
    public override string Message
    {
        get
        {
            return "The item not found";
        }
    }
}

public class AllreadyExistException : Exception
{
    //Overriding the Message property
    public override string Message
    {
        get
        {
            return "The ID number already exists";
        }
    }
}

////using System;
//namespace ExceptionHandlingDemo
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            int Number1, Number2, Result;
//            try
//            {
//                Console.WriteLine("Enter First Number:");
//                Number1 = int.Parse(Console.ReadLine());
//                Console.WriteLine("Enter Second Number:");
//                Number2 = int.Parse(Console.ReadLine());
//                if (Number2 % 2 > 0)
//                {
//                    //OddNumberException ONE = new OddNumberException();
//                    //throw ONE;
//                    throw new OddNumberException();
//                }
//                Result = Number1 / Number2;
//                Console.WriteLine(Result);
//            }
//            catch (OddNumberException one)
//            {
//                Console.WriteLine($"Message: {one.Message}");
//                Console.WriteLine($"HelpLink: {one.HelpLink}");
//                Console.WriteLine($"Source: {one.Source}");
//                Console.WriteLine($"StackTrace: {one.StackTrace}");
//            }
//            Console.WriteLine("End of the Program");
//            Console.ReadKey();
//        }
//    }
//}
////