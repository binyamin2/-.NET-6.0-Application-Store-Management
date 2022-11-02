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

    ///metodes Add organ
    private static void AddOrder(DalOrder NewOrder)
    {
        ArrOrder[Config.OrderIndex] = NewOrder;
        Config.OrderIndex++;
    }
    
    private static void AddOrderItem(DalOrderItem NewOrderItem)
    {
        ArrOrderItem[Config.OrderItemIndex] = NewOrderItem;
        Config.OrderItemIndex++;
    }

    private static void AddProduct(DalProduct NewProduct)
    {
        ArrProduct[Config.ProductIndex] = NewProduct;
        Config.ProductIndex++;
    }

    internal static class Config
    {
        ///index for array

        internal static int OrderIndex = 0;

        internal static int OrderItemIndex = 0;

        internal static int ProductIndex = 0;

        private static int IDOrder = 208900;
        
       internal static void getIDOrder()
        {
            IDOrder++;          
        }

        private static int IDOrderItem = 1;

        internal static void getID_OI()
        {
            IDOrderItem++;
        }

    }


}




