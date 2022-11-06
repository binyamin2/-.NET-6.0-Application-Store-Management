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

    internal static OrderItem[] ArrOrderItem = new OrderItem[200];

    internal static Order[] ArrOrder = new Order[100];

    internal static Product[] ArrProduct = new Product[50];


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
        string[] ProdName = new string[20] { "SportAdids","SportNike", "SportPuma","SportNewBalance",
                                         "HikeSalomon","HikeRedback","HikeTeva","HikeNewBalance",
                                         "SandalsSource","SandalsChcko","SandalsTeva","SandalsCrocs",
                                         "ElegantBlandstone","ElegantNike ","ElegantGolf","ElegantCastro",
                                         "BootsRedback","BootsBlandstone", "BootsKangeru ","BootsNogaEinat" };
        int[] price = new int[20] {250,300,300,350,200,400,350,300, 300, 350,
                                  300, 300, 350, 300, 300, 350 , 350, 300, 300, 350 };
        int[] id = new int[20] { 111111, 111112, 111113, 111114, 111115, 111116, 111117, 111118, 111119,
                                111120, 111121, 111122, 111123, 111124, 111125, 111126, 111127, 111128,
                                111129, 111130 };
        int[] inStock = new int[20] { 30, 90, 80, 70, 60, 70, 80, 90, 0,
                                      55, 45, 35, 65, 75, 85, 25, 5, 60, 75, 35 };
        int j = 0;
        DalProduct dalP = new DalProduct();
        for (int i = 0; i < 5; i++)
        {
            for (int r = 0; r < 4; r++,j++)
            {
                Product p = new Product();
                p.ID = id[j];
                p.Name = ProdName[j];
                p.Category = (Category)i;
                p.Price = price[j];
                p.InStock= inStock[j];
                dalP.AddProduct(p);
            }

        }
    
    
    }

    private static void Order_Initialize()
    {
        DalOrder dalOrder1 = new DalOrder();

        Order order1 = new Order();

       string CustomerName = "a";

       int index_address = 1;

       string CustomerAdress = "king_gorg";

        for (int i = 0; i < 20; i++)
        {
            order1.CustomerName = CustomerName;
          
            order1.CustomerEmail = CustomerName += "@gmail.com";

            CustomerName += "b";

            order1.CustomerAdress = CustomerAdress += index_address;

            index_address++;


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




