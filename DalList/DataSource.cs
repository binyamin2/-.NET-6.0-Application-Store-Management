using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal static class DataSource
{
    public readonly static Random RandomInt = new Random();

    ///Array

    internal static DalOrderItem[] ArrOrderItem = new DalOrderItem[200];

    internal static DalOrder[] ArrOrder = new DalOrder[100];

    internal static DalProduct[] ArrProduct = new DalProduct[50];


    static DataSource()
    {
        s_Initialize();
    }


    private static void  s_Initialize()
    {

        ///inital product
        ///
        ///inital order
        ///
        ///inital orderItem
    }








    ///index for array

    private static int OrderIndex = 0;

    private static int OrderItemIndex = 0;

    private static int ProductIndex = 0;

    ///metodes Add organ
    private static void AddOrder(DalOrder NewOrder)
    {
        ArrOrder[OrderIndex] = NewOrder;
        OrderIndex++;
    }
    
    private static void AddOrderItem(DalOrderItem NewOrderItem)
    {
        ArrOrderItem[OrderItemIndex] = NewOrderItem;
        OrderItemIndex++;
    }

    private static void AddProduct(DalProduct NewProduct)
    {
        ArrProduct[ProductIndex] = NewProduct;
        ProductIndex++;
    }



    
}




