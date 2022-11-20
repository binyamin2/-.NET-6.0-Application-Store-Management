using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;



[Serializable]
public class NotFoundException : Exception
{
    public NotFoundException() : base() { }
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string message, Exception inner) : base(message, inner) { }
    protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public override string Message
    {
        get
        {
            return "The item not found";
        }
    }
}

[Serializable]
public class AllreadyExistException : Exception
{
    public AllreadyExistException() : base() { }
    public AllreadyExistException(string message) : base(message) { }
    public AllreadyExistException (string message, Exception inner) : base(message, inner) { }
    protected AllreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }

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