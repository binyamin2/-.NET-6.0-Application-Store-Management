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


        Product_Initialize();
        ///
        Order_Initialize();
        ///
        OrderItem_Initialize();
    }

    private static void Product_Initialize()
    {

        for (int i = 0; i < 16; i++)
        {
            

        }
    
    
    }

    private static void Order_Initialize()
    {
        DalOrder dalOrder1 = new DalOrder();

        

       string CustomerName = "a";

       int index_address = 1;

       string CustomerAdress = "king_gorg";

       int index_time_delivery = 0;
       int index_time_ship = 0;


        for (int i = 0; i < 20; i++)
        {
            Order order1 = new Order();

            order1.CustomerName = CustomerName;
          
            order1.CustomerEmail = CustomerName += "@gmail.com";

            CustomerName += "b";

            order1.CustomerAdress = CustomerAdress += index_address;

            index_address++;

            order1.OrderDate = DateTime.Now;

            if (index_time_ship < 16)
            {
                order1.ShipDate = new DateTime(2023, 1, 1);
                index_time_ship++;


                if (index_time_delivery < 10)
                {
                    order1.ShipDate = new DateTime(2023, 2, 1);
                    index_time_delivery++;
                }
            }


            dalOrder1.Add(order1);
        }


    }

    private static void OrderItem_Initialize()
    {
        DalOrderItem dalOrderItem = new DalOrderItem();

        Random rnd = new Random();
        rnd.Next(10, 20);

        int indexOrder = -1;


        for (int i = 0; i < 40; i++)
        {
            OrderItem OI = new OrderItem();

            OI.ProdectID = rnd.Next(111111, 111131);

            if (i % 4 == 0 )
            {
                indexOrder++;
            }
            OI.OrderItemID = indexOrder;
            OI.Amount = rnd.Next(1, 20);

            for (int i1 = 0; i1 < DataSource.Config.ProductIndex; i1++)
            {
                if (DataSource.ArrProduct[i1].ID == OI.ProdectID)
                    OI.Price = DataSource.ArrProduct[i1].Price;
            }

            dalOrderItem.AddOrderItem(OI);

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




