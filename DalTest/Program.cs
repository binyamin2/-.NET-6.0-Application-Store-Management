﻿using DO;
using Dal;
using System.Collections.Generic;
using DalApi;
using System.Xml.Linq;

namespace Dal;

/// <summary>
/// binyamin shapira:208965863
/// oz asban:207565607
/// file:Program
/// discraption:
/// this file make tests
/// </summary>
enum StoreItem { Exit, Product, Order, OrderItem, InitXMlFiles };
enum CraudMethod { Add = 1, View, ViewAll, Update, Delete, };
enum ItemOrderMenu { Add = 1, View, ViewAll, Update, Delete, itemByOrderAndProduct, itemListByOrder };


class Program
{
    static IDal? DalList = Factory.Get();
    private static StoreItem menuCode;
    static void Main(string[] args)
    {

        Console.WriteLine(@"Welcome to the 'Shoes for everyone' shoe StoreItem");
        do
        {
            Console.WriteLine(@"For any actions about Products press 1");
            Console.WriteLine(@"For any actions about Orders press 2");
            Console.WriteLine(@"For any actions about Order Items press 3");
            Console.WriteLine(@"For exit the menu press 0");
            StoreItem.TryParse(Console.ReadLine(), out menuCode);
            try
            {
                switch (menuCode)
                {
                    case StoreItem.Product:
                        ProductMenue();
                        break;
                    case StoreItem.Order:
                        OrderMenue();
                        break;
                    case StoreItem.OrderItem:
                        OrderItemMenue();
                        break;
                    case StoreItem.Exit:
                        return;
                    case StoreItem.InitXMlFiles:
                        // XMLTools.SaveListToXMLSerializer<DO.Product>(DalList.Product.GetAll().ToList(), "Product");
                        List<OrderItem?> list = DalList.OrderItem.GetAll()?.ToList();
                        XMLTools.SaveListToXMLSerializer<DO.Order>(DalList.Order.GetAll().ToList(), "Orders");
                        XMLTools.SaveListToXMLSerializer<DO.Product>(DalList.Product.GetAll().ToList(), "Products");
                        XMLTools.SaveListToXMLSerializer<DO.OrderItem>(list, "OrderItems");
                        XElement ConfigRootElem = XMLTools.LoadListFromXMLElement("Config");
                        ConfigRootElem.Element("OrderItemIndex")?.SetValue(40);
                        ConfigRootElem.Element("OrderIndex")?.SetValue(20);
                        XMLTools.SaveListToXMLElement(ConfigRootElem, "Config");
                        

                        break;
                    default:
                        throw new Exception("Unvalide choice press any key to continue...");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } while (menuCode != StoreItem.Exit);
    }

    static void ProductMenue()
    {
        Console.WriteLine(@"For add a product tap 1");
        Console.WriteLine(@"For product display tap 2");
        Console.WriteLine(@"For display all products tap 3");
        Console.WriteLine(@"For product update tap 4");
        Console.WriteLine(@"For product deletion tap 5");

        CraudMethod productCode;
        CraudMethod.TryParse(Console.ReadLine(), out productCode);
        switch (productCode)
        {
            case CraudMethod.Add:
                Console.WriteLine(@"Enter the product ID number that you want to add");
                int id;
                int.TryParse(Console.ReadLine(), out id);
                Console.WriteLine(@"Enter a product name that you want to add");
                string name;
                name = Console.ReadLine();
                Console.WriteLine(@"Enter a product category that you want to add");
                Console.WriteLine(
                $@"
                Sport = 0
                Hike =  1
                Sandals = 2
                Elegant = 3
                Boots = 4 ");
                int catgory;
                int.TryParse(Console.ReadLine(), out catgory);

                Console.WriteLine(@"Enter a product price that you want to add");
                double tmpPrice;
                double.TryParse(Console.ReadLine(), out tmpPrice);
                int tmpAmount;
                Console.WriteLine(@"Enter a product Amount in stok that you want to add");
                int.TryParse(Console.ReadLine(), out tmpAmount);
                Product addedProduct = new Product()
                {
                    ID = id,
                    Name = name,
                    Category = (Category)catgory,
                    Price = tmpPrice,
                    InStock= tmpAmount
                };
                int? addedProductId = DalList.Product.Add(addedProduct);
                Console.WriteLine("The Product id: {0}", addedProductId);
                break;
            case CraudMethod.View:
                Console.WriteLine(@"Enter the product ID number that you want to see his details");
                int.TryParse(Console.ReadLine(), out id);
                Product? productToShow = new Product();
                productToShow = DalList.Product.Get(id);
                Console.WriteLine(productToShow);
                break;
            case CraudMethod.ViewAll:
                IEnumerable<Product?> productsList = DalList.Product.GetAll();
                foreach (var item in productsList)
                {
                    Console.WriteLine(item);
                }
                
                break;
            case CraudMethod.Update:
                Console.WriteLine(@"Enter the product ID number that you want to update his details");
                int.TryParse(Console.ReadLine(), out id);
                Product? oldProduct = DalList.Product.Get(id);
                Console.WriteLine(oldProduct);
                Console.WriteLine(@"Enter the new name");
                name = Console.ReadLine();
                Console.WriteLine(@"Enter the new category");
                Console.WriteLine(
                $@"
                Sport = 0
                Hike =  1
                Sandals = 2
                Elegant = 3
                Boots = 4 ");
                int tmpcatgory;
                int.TryParse(Console.ReadLine(), out tmpcatgory);
                Console.WriteLine(@"Enter The New new price");
                double tmpPrice2;
                double.TryParse(Console.ReadLine(), out tmpPrice2);
                int tmpAmount2;
                Console.WriteLine(@"Enter the new amount in stok");
                int.TryParse(Console.ReadLine(), out tmpAmount);
                Product Update = new Product()
                {
                    ID = id,
                    Name = name,
                    Category = (Category)tmpcatgory,
                    Price = tmpPrice2,
                    InStock= tmpAmount
                };
                DalList.Product.Update(Update);
                break;
            case CraudMethod.Delete:
                Console.WriteLine(@"Enter the product ID number that you want to remove");
                int.TryParse(Console.ReadLine(), out id);
                DalList.Product.Delete(id);
                break;
            default:
                break;
        }
    }
    static void OrderMenue()
    {
        
        Console.WriteLine(@"For add a Order tap 1");
        Console.WriteLine(@"For Order display tap 2");
        Console.WriteLine(@"For display all Orders tap 3");
        Console.WriteLine(@"For Order update tap 4");
        Console.WriteLine(@"For Order deletion tap 5");

        CraudMethod orderCode;
        int id;
        string customeName;
        string email;
        string adress;
        DateTime orderCreate;
        DateTime orderShip;
        DateTime delivery;
        CraudMethod.TryParse(Console.ReadLine(), out orderCode);
        switch (orderCode)
        {
            case CraudMethod.Add:
                Console.WriteLine(@"Enter a Customer name that you want to add");
                customeName = Console.ReadLine();
                Console.WriteLine(@"Enter a Customer email that you want to add");
                email = Console.ReadLine();
                Console.WriteLine(@"Enter a Customer adress that you want to add");
                adress = Console.ReadLine();
                orderCreate = DateTime.Now;
                Order addedOrder = new Order()
                {
                    ID= 0,
                    CustomerAdress = adress,
                    CustomerEmail = email,
                    CustomerName = customeName,
                    OrderDate = orderCreate,
                    ShipDate = null,
                    DeliveryDate = null
                };
                int? addedOrderId = DalList.Order.Add(addedOrder);
                Console.WriteLine("The Order id: {0}", addedOrderId);
                break;
            case CraudMethod.View:
                Console.WriteLine(@"Enter the Order ID number that you want to see his details");
                while (!int.TryParse(Console.ReadLine(), out id)) ;
                Order? OrderToShow = new Order();
                OrderToShow = DalList.Order.Get(id);
                Console.WriteLine(OrderToShow);
                break;
            case CraudMethod.ViewAll:
                IEnumerable<Order?> productsList = DalList.Order.GetAll();
                foreach (var item in productsList)
                {
                    Console.WriteLine(item);
                }
                break;
            case CraudMethod.Update:
                Console.WriteLine(@"Enter the Order ID number that you want to update his details");
                int.TryParse(Console.ReadLine(), out id);
                Order? oldOrder = DalList.Order.Get(id);
                Console.WriteLine(oldOrder);
                Console.WriteLine(@"Enter the new CUSTOMER name");
                customeName = Console.ReadLine();
                Console.WriteLine(@"Enter the new CUSTOMER email");
                email = Console.ReadLine();
                Console.WriteLine(@"Enter the new CUSTOMER adress");
                adress = Console.ReadLine();
                Console.WriteLine(@"Enter the new create date");
                DateTime.TryParse(Console.ReadLine(), out orderCreate);
                Console.WriteLine(@"Enter the new ship date");
                DateTime.TryParse(Console.ReadLine(), out orderShip);
                Console.WriteLine(@"Enter the new delivery date");
                DateTime.TryParse(Console.ReadLine(), out delivery);

                Order updateOrder = new Order()
                {
                    ID = id,
                    CustomerAdress = adress,
                    CustomerEmail = email,
                    CustomerName = customeName,
                    OrderDate = orderCreate,
                    ShipDate = orderShip,
                    DeliveryDate = delivery
                };
                DalList.Order.Update(updateOrder);
                break;
            case CraudMethod.Delete:
                Console.WriteLine(@"Enter the Order ID number that you want to remove");
                int.TryParse(Console.ReadLine(), out id);
                DalList.Order.Delete(id);
                break;
            default:
                break;
        }
    }
    static void OrderItemMenue()
    {
        Console.WriteLine(@"For add a Order Item tap 1");
        Console.WriteLine(@"For Order Item display tap 2");
        Console.WriteLine(@"For display all OrderItems tap 3");
        Console.WriteLine(@"For Order Item update tap 4");
        Console.WriteLine(@"For Order item deletion tap 5");
        Console.WriteLine(@"For display Order item by order id and product id tap 6");
        Console.WriteLine(@"For display Order item list belongs to an order tap 7");

        ItemOrderMenu OrderItem;
        int id;
        int productId;
        int orderId;
        int Amount;
        double price;
        ItemOrderMenu.TryParse(Console.ReadLine(), out OrderItem);
        switch (OrderItem)
        {
            case ItemOrderMenu.Add:

                Console.WriteLine(@"Enter the productID number that you want to add");
                while (!int.TryParse(Console.ReadLine(), out productId)) ;
                Console.WriteLine(@"Enter the Order Id that you want to add");
                while (!int.TryParse(Console.ReadLine(), out orderId)) ;
                Console.WriteLine(@"Enter the amount that you want to add");
                while (!int.TryParse(Console.ReadLine(), out Amount)) ;
                Console.WriteLine(@"Enter the price of product");
                while (!double.TryParse(Console.ReadLine(), out price)) ;

                OrderItem addedOrderItem = new OrderItem()
                {
                    ID = 0,///the function add give him id OI
                    ProductID = productId,
                    OrderID = orderId,
                    Amount = Amount,
                    Price = price
                };
                int? addedOrderId = DalList.OrderItem.Add(addedOrderItem);
                Console.WriteLine("The Order item id: {0}", addedOrderId);
                break;
            case ItemOrderMenu.View:
                Console.WriteLine(@"Enter the Order Item ID number that you want to see his details");
                while (!int.TryParse(Console.ReadLine(), out orderId)) ;
                OrderItem? OrderItemToShow = new OrderItem();
                OrderItemToShow = DalList.OrderItem.Get(orderId);
                Console.WriteLine(OrderItemToShow);
                break;
            case ItemOrderMenu.ViewAll:
                IEnumerable<OrderItem?> OrdersItemList = DalList.OrderItem.GetAll();
                foreach (var item in OrdersItemList)
                {
                    Console.WriteLine(item);
                }
                break;
            case ItemOrderMenu.Update:
                Console.WriteLine(@"Enter the Order ID number that you want to update his details");
                while (!int.TryParse(Console.ReadLine(), out orderId)) ;
                OrderItem? oldItemOrder = DalList.OrderItem.Get(orderId);
                Console.WriteLine(oldItemOrder);
                
                Console.WriteLine(@"Enter the productID number that you want to update");
                while (!int.TryParse(Console.ReadLine(), out productId)) ;
                Console.WriteLine(@"Enter the orderID that you want to update");
                while (!int.TryParse(Console.ReadLine(), out orderId)) ;
                Console.WriteLine(@"Enter the amount that you want to update");
                while (!int.TryParse(Console.ReadLine(), out Amount)) ;
                Console.WriteLine(@"Enter the price of product");
                while (!double.TryParse(Console.ReadLine(), out price)) ;
                OrderItem updateItemOrder = new OrderItem()
                {
                    ID = (int)oldItemOrder?.ID,
                    ProductID= productId,
                    OrderID = orderId,
                    Amount = Amount,
                    Price = price
                };
                DalList.OrderItem.Update(updateItemOrder);
                break;
            case ItemOrderMenu.Delete:
                Console.WriteLine(@"Enter the Order ID number that you want to remove");
                int.TryParse(Console.ReadLine(), out orderId);
                DalList.OrderItem.Delete(orderId);
                break;
            case ItemOrderMenu.itemByOrderAndProduct:
                Console.WriteLine(@"Enter the Order ID number that you want to see his details");
                int.TryParse(Console.ReadLine(), out orderId);
                Console.WriteLine(@"Enter the product ID number that you want to see his details");
                int.TryParse(Console.ReadLine(), out productId);
                OrderItem? itemToShowByProductAndOrder = DalList.OrderItem.GetItemByOrderAndProduct(orderId, productId);
                Console.WriteLine(itemToShowByProductAndOrder);
                break;
            case ItemOrderMenu.itemListByOrder:
                Console.WriteLine(@"Enter the Order ID number that you want to see Item Order that belogs to him");
                int.TryParse(Console.ReadLine(), out orderId);
                IEnumerable<OrderItem?> ordersItemsList = DalList.OrderItem.GetItemsListByOrderId(orderId);
                foreach (OrderItem item in ordersItemsList)
                    Console.WriteLine(item);
                break;
            default:
                break;
        }
    }
}
