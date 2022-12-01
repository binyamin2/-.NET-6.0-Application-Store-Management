using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BlImplementation;

internal static class CopyProperties<T, K>

{/// <summary>
/// method to copy detauls from entities to other entities
/// </summary>
/// <param name="child"></param>
/// <param name="parent"></param>
    public static void Copy(ref T? child , K? parent)
    {
        //get the proprties list
        var parentProperties = parent.GetType().GetProperties();
        var childProperties = child.GetType().GetProperties();
        //update the value of property is have the same name and type
        foreach (var parentProperty in parentProperties)
        {
            foreach (var childProperty in childProperties)
            {
                if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
                {
                 
                    object b = RuntimeHelpers.GetObjectValue(child);
                    childProperty.SetValue( b, parentProperty.GetValue(parent, null));
                    child = (T?)b;
                    break;
                }
            }
        }
    }



}


