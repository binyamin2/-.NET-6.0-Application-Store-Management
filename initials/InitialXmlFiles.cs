using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

public class InitialXmlFiles
{
    public readonly static Random RandomInt = new Random();
    static void Main(string[] args)
    {

        string ProductsPath = @"C:\Users\binyamin\source\repos\binyamin2\dotNet5783_5863_5607\xml\Product.xml";
        string OrdersPath = @"C:\Users\binyamin\source\repos\binyamin2\dotNet5783_5863_5607\xml\Order.xml";
        string OrderItemsPath = @"C:\Users\binyamin\source\repos\binyamin2\dotNet5783_5863_5607\xml\OrderItem.xml";
        XElement products;
        XElement orders;
        XElement orderItems;
        try
        {
            products = XElement.Load(ProductsPath);
            orders = XElement.Load(OrdersPath);
            //orderItems= XElement.Load(OrderItemsPath);
        }
        catch (Exception)
        {
            Console.WriteLine("The file didn't open");
            
        }

        products = new XElement("Products");
        orders = new XElement("orders");
        orderItems = new XElement("OrderItems");
        #region product init
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


        for (int i = 0; i < 5; i++)
        {
            for (int r = 0; r < 4; r++, j++)
            {
                XElement ID = new XElement("ID",id[j]);
                XElement name = new XElement("Name", ProdName[j]);
                XElement category = new XElement("Category", (Category)i);
                XElement Price = new XElement("Price" ,price[j]);
                XElement amount = new XElement("InStock", inStock[j]);
                XElement Prod = new XElement("Product", ID,name, category, Price,amount);
                products.Add(Prod);
            }

        }
        products.Save(ProductsPath);
        #endregion
        #region order init
        string[] CustomerName = {"avi","dani","oz","yoni","gal","david","yona","bini","meir","yosi","efi","ynon",
        "slomi", "yarden","shchr","roni","tami","hdar","zav","nati"};

        int index_address = 1;

        string CustomerAdress = "king_gorg";

        int index_time_delivery = 0;
        int index_time_ship = 0;


        for (int i = 0; i < 20; i++)
        {
            XElement ID=new XElement("ID", i);
            XElement customerName = new XElement ("CustomerName",CustomerName[i]);

            XElement customerEmail = new XElement("CustomerEmail", CustomerName[i] += "@gmail.com");

            XElement customerAdress = new XElement("CustomerAdress", CustomerAdress += index_address.ToString());

            index_address++;

            DateTime Shipi = new DateTime();
            XElement shipDate=new XElement("SSh");
            XElement? deliveryDate= new XElement("ssh");
            XElement orderDate = new XElement("ssh");
            ///initial the date
            if (index_time_ship < 16)
            {
                index_time_ship++;

                if (index_time_delivery < 10)
                {
                    DateTime Deli = DateTime.Now - new TimeSpan(RandomInt.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 100L));
                    Shipi = Deli - new TimeSpan(RandomInt.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 7L));
                    deliveryDate = new XElement("deliveryDate",Deli);
                    shipDate =  new XElement ("ShipDate",Deli - Shipi);
                    index_time_delivery++;
                }
                else
                {
                    Shipi = DateTime.Now - new TimeSpan(RandomInt.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 7L));
                    shipDate = new XElement ("ShipDate",Shipi);
                }
                 orderDate = new XElement ("OrderDate",Shipi - new TimeSpan(RandomInt.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 3L)));
            }
            else
            {
                 orderDate = new XElement("OrderDate",DateTime.Now - new TimeSpan(RandomInt.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L)));
            }

                XElement order = new XElement("Order", ID, customerName, customerEmail, customerAdress, shipDate, deliveryDate, orderDate);

            orders.Add(order);
        }
        #endregion

    }


    public enum Category
    {
        Sport,
        Hike,
        Sandals,
        Elegant,
        Boots
    }




}
