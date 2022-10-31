using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal static class DataSource
{
    public readonly static Random RandomInt = new Random();

    internal static DalOrderItem[]  ArrOrderItem  = new DalOrderItem[200];

    internal static DalOrder[] ArrOrder = new DalOrder[100];

    internal static DalProduct[] ArrProudct = new DalProduct[50];

}




