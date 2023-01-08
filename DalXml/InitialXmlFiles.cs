using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DO;

namespace Dal;

public class InitialXmlFiles
{
    static void Main(string[] args)
    {
        string ProductsPath = @"..\xml\Product.xml";
        XElement products;
        try
        {
            products = XElement.Load(ProductsPath);
        }
        catch (Exception)
        {

            Console.WriteLine("The file didn't open");
        }

        products = new XElement("Products");

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
                XElement category = new XElement("Category", (DO.Category)i);
                XElement Price = new XElement("Price" ,price[j]);
                XElement Instock = new XElement("InStock", inStock[j]);
                XElement Prod = new XElement("Product", ID,name, category, Price,inStock);
                products.Add(ID);
            }

        }
        products.Save(ProductsPath);


    }

    




}
