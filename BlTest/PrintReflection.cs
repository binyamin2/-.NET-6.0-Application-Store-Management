using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlTest;

internal static class PrintReflection<T>
{
    public static void printAllProperties(T? obj)
    {
        Console.WriteLine('\t');
        foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
        {
            
            string name = descriptor.Name;
            object value = descriptor.GetValue(obj);
            Console.WriteLine("     {0} = {1}", name, value);
            
        }

    }
}
