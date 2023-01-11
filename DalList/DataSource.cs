using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using DO;
namespace Dal;

/// <summary>
/// binyamin shapira:208965863
/// oz asban:207565607
/// file:DataStructure
/// discraption:
/// this file contain all the Data base
/// </summary>

internal static class DataSource
{


    public readonly static Random RandomInt = new Random();

    ///Array

    internal static List<OrderItem?> LOrderItem = new List<OrderItem?>();

    internal static List<Order?> LOrder = new List<Order?>();

    internal static List<Product?> LProduct = new List<Product?>();


    static DataSource() ///static constractor
    {
      
        s_Initialize();
       
    }

    /// <summary>
    /// metouds of Initialize data base
    /// </summary>
    private static void  s_Initialize()
    {


        Product_Initialize();
        
        Order_Initialize();
        
        OrderItem_Initialize();
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
                dalP.Add(p);
            }

        }
 
    }

    private static void Order_Initialize()
    {
        DalOrder dalOrder1 = new DalOrder();

        

       string[] CustomerName = {"avi","dani","oz","yoni","gal","david","yona","bini","meir","yosi","efi","ynon",
        "slomi", "yarden","shchr","roni","tami","hdar","zav","nati"};

       int index_address = 1;

       string CustomerAdress = "king_gorg";

       int index_time_delivery = 0;
       int index_time_ship = 0;


        for (int i = 0; i < 20; i++)
        {
            Order order1 = new Order();

            order1.CustomerName = CustomerName[i];
          
            order1.CustomerEmail = CustomerName[i] += "@gmail.com";

            CustomerAdress = "king_gorg";

            order1.CustomerAdress = CustomerAdress += index_address;

            index_address++;

            
            ///initial the date
            if (index_time_ship < 16)
            {    
                index_time_ship++;

                if (index_time_delivery < 10)
                {
                    order1.DeliveryDate = DateTime.Now - new TimeSpan(RandomInt.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 100L));
                    order1.ShipDate = order1.DeliveryDate - new TimeSpan(RandomInt.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 7L));
                    index_time_delivery++;
                }
                else
                {

                    order1.ShipDate = DateTime.Now - new TimeSpan(RandomInt.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 7L));
                }
                order1.OrderDate = order1.ShipDate - new TimeSpan(RandomInt.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 3L));
            }
            else
            {
                order1.OrderDate = DateTime.Now - new TimeSpan(RandomInt.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L));

            }


            dalOrder1.Add(order1);
        }


    }

    private static void OrderItem_Initialize()
    {
        DalOrderItem dalOrderItem = new DalOrderItem();

        Random rnd = new Random();

        int indexOrder = -1;


        for (int i = 0; i < 40; i++)
        {
            OrderItem OI = new OrderItem();
            OI.ID = i;
            OI.ProductID = rnd.Next(111111, 111131);

            if ((float)i % 2.0 == 0 )
            {
                indexOrder++;
            }
            OI.OrderID = indexOrder;
            OI.Amount = rnd.Next(1, 20);

            ///chack the price of the choosen prodect
            foreach (var item in DataSource.LProduct)
            {
                if(item?.ID==OI.ProductID)
                    OI.Price = (double)item?.Price;
            }
            
            ///put the new organ into the array
            dalOrderItem.Add(OI);

        }


    }



    ///metodes Add organ
    internal static void AddOrder(Order NewOrder)
    {
        LOrder.Add(NewOrder);
    }
    
    internal static void Add(OrderItem NewOrderItem)
    {
        LOrderItem.Add (NewOrderItem);
    }

   internal static void Add(Product NewProduct)
    {
        LProduct.Add(NewProduct);
    }

 
        ///index for array

       

        internal static int IDOrder = 0;

    /// <summary>
    /// metouds of get ID array
    /// </summary>
        
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




