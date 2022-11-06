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
    internal static void AddOrder(Order NewOrder)
    {
        ArrOrder[Config.OrderIndex] = NewOrder;
        Config.OrderIndex++;
    }
    
    internal static void AddOrderItem(OrderItem NewOrderItem)
    {
        ArrOrderItem[Config.OrderItemIndex] = NewOrderItem;
        Config.OrderItemIndex++;
    }

   internal static void AddProduct(Product NewProduct)
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

        internal static int IDOrder = 0;


        
       internal static void getIDOrder()
        {
            IDOrder++;          
        }

        internal static int IDOrderItem = 0;

        internal static void getID_OI()
        {
            IDOrderItem++;
        }

    }


}




