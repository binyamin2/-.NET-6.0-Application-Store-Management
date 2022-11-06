using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using DO;
namespace Dal;


internal static class DataSource
{
    public readonly static Random RandomInt = new Random();

    ///Array

    internal static List<OrderItem> ArrOrderItem = new List <OrderItem>();

    internal static List<Order> ArrOrder = new List<Order>();

    internal static List<Product> ArrProduct = new List<Product>();


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

    private static void Product_Initialize()
    {

        for (int i = 0; i < 10; i++)
        {

        }
    
    
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




