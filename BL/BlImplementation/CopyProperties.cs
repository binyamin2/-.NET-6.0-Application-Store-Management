using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    internal class CopyProperties<T, K>

    {
        public static void Copy(T? child, K? parent)
        {
            var parentProperties = parent.GetType().GetProperties();
            var childProperties = child.GetType().GetProperties();

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
}
