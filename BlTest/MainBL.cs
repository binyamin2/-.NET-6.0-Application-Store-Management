using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using Dal;
using DO;

namespace BlTest;
enum BLoption { Cart = 1, Order, Product, Exit = 0 }
enum BLorder { GetListOrder = 1, GetOrder, UpdateShip, UpdateDelivery, TrackOrder, AddOrder, DeleteOrder, ChangeAmount, Exit = 0}

enum BLcart { AddProductToCart = 1, UpdateAmpuntProduct, ConfirmOrder, Exit =0 }

enum BLproduct { GetListProduct = 1, GetProduct, GetDetailsProduct, AddProduct, DeleteProduct, UpdateDetailsProduct,Exit =0 }
class MainBL
{
    static BlApi.IBl iblogic = new BlImplementation.Bl();

    private static BLoption menuCode;
    static Cart Maincart = new Cart(); 

    static void Main(string[] args)
    {
        do
        {
            Console.WriteLine(@"For any actions about Cart press 1");
            Console.WriteLine(@"For any actions about Orders press 2");
            Console.WriteLine(@"For any actions about Product Items press 3");
            Console.WriteLine(@"For exit the menu press 0");
            BLoption.TryParse(Console.ReadLine(), out menuCode);

            try
            {
                switch (menuCode)
                {
                    case BLoption.Cart:
                        CartMenue();
                        break;
                    case BLoption.Order:
                        OrderMenue();
                        break;
                    case BLoption.Product:
                        ProductMenue();
                        break;
                    case BLoption.Exit:

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

        } while (menuCode != BLoption.Exit);

    }

    static void ProductMenue()
    {

        BLproduct productCode = new BLproduct();
        

        do
        {
            Console.WriteLine(@"For get list tap 1");
            Console.WriteLine(@"For get product manager tap 2");
            Console.WriteLine(@"For get details product client tap 3");
            Console.WriteLine(@"For add product tap 4");
            Console.WriteLine(@"For product deletion tap 5");
            Console.WriteLine(@"For Update Details Product tap 6");
            Console.WriteLine(@"For return to menu tap 0 ");
            int id;
            BLproduct.TryParse(Console.ReadLine(), out productCode);
            try
            {
                switch (productCode)
                {
                    case BLproduct.GetListProduct:
                        IEnumerable <BO.ProductForList> ProductList = iblogic.Product.GetList();
                        foreach (var item in ProductList)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case BLproduct.GetProduct:
                        
                        Console.WriteLine(@"Enter the product ID number that you want to see his details");
                        int.TryParse(Console.ReadLine(), out id);
                        BO.Product product = iblogic.Product.GetForManager(id);
                        Console.WriteLine(product);
                        break;
                    case BLproduct.GetDetailsProduct:
          
                        Console.WriteLine(@"Enter the product ID number that you want to see his details");
                        int.TryParse(Console.ReadLine(), out id);
                        BO.ProductItem product1 = iblogic.Product.GetForClient(id, Maincart);///what??
                        Console.WriteLine(product1);
                        break;

                    case BLproduct.AddProduct:
                        Console.WriteLine(@"Enter the product ID number that you want to add");
                        int id1;
                        int.TryParse(Console.ReadLine(), out id1);
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
                        BO.Product addedProduct = new BO.Product()
                        {
                            ID = id1,
                            Name = name,
                            Category = (Category)catgory,
                            Price = tmpPrice,
                            InStock = tmpAmount
                        };

                        iblogic.Product.Add(addedProduct);
                        Console.WriteLine(addedProduct);


                        break;
                    case BLproduct.DeleteProduct:
                        Console.WriteLine(@"Enter the product ID number that you want to delete");
                        int.TryParse(Console.ReadLine(), out id);
                        iblogic.Product.Delete(id);
                       

                        break;
                    case BLproduct.UpdateDetailsProduct:
                        int id2;
                        Console.WriteLine(@"Enter the product ID number that you want to update his details");
                        int.TryParse(Console.ReadLine(), out id2);
                        BO.Product oldProduct = iblogic.Product.GetForManager(id2);
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
                        BO.Product Update = new BO.Product()
                        {
                            ID = id2,
                            Name = name,
                            Category = (Category)tmpcatgory,
                            Price = tmpPrice2,
                            InStock = tmpAmount
                        };
                        iblogic.Product.Update(Update);
                        ///print update
                        ///product
                        Console.WriteLine(iblogic.Product.GetForManager(id2));

                        break;
                    case BLproduct.Exit:
                        break;
                    default:
                        throw new Exception("invalide choice press any key to continue...");
                        
                }


            }




            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }




        } while (productCode != BLproduct.Exit);

        
    }

    static void OrderMenue()
    {

        BLorder orderCode = new BLorder();
        do
        {
            Console.WriteLine(@"For get list tap 1");
            Console.WriteLine(@"For get order tap 2");
            Console.WriteLine(@"For update ship tap 3");
            Console.WriteLine(@"For Update Delivery tap 4");
            Console.WriteLine(@"For Track Order tap 5");
            Console.WriteLine(@"For Add Order tap 6");
            Console.WriteLine(@"For Delete Order tap 7");
            Console.WriteLine(@"For Change Amount tap 8");
            Console.WriteLine(@"For return to menu tap 0 ");

            BLorder.TryParse(Console.ReadLine(), out orderCode);
            int id;
           

            BO.Order OrderToShow;
            try
            {

                switch (orderCode)
                {
                    case BLorder.GetListOrder:

                        IEnumerable<BO.OrderForList> OrderList = iblogic.Order.GetList();
                        foreach (var item in OrderList)
                        {
                            Console.WriteLine(item);
                        }

                        break;
                    case BLorder.GetOrder:
                        Console.WriteLine(@"Enter the Order ID number that you want to see his details");
                        while (!int.TryParse(Console.ReadLine(), out id)) ;
                        OrderToShow = iblogic.Order.Get(id);
                        Console.WriteLine(OrderToShow);
                        break;
                    case BLorder.UpdateShip:
                        Console.WriteLine(@"Enter the Order ID number that you want to update the ship order");
                        while (!int.TryParse(Console.ReadLine(), out id)) ;
                        OrderToShow = iblogic.Order.UpdateShip(id);
                        Console.WriteLine(OrderToShow);
                        break;
                    case BLorder.UpdateDelivery:
                        Console.WriteLine(@"Enter the Order ID number that you want to update the Delivery order");
                        while (!int.TryParse(Console.ReadLine(), out id)) ;
                        OrderToShow = iblogic.Order.UpdateDelivery (id);
                        Console.WriteLine(OrderToShow);
                        break;
                    case BLorder.TrackOrder:
                        Console.WriteLine(@"Enter the Order ID number that you want to tracking");
                        while (!int.TryParse(Console.ReadLine(), out id)) ;
                        BO.OrderTracking OrderTracking = iblogic.Order.OrderTracking(id);
                        Console.WriteLine(OrderTracking);

                        break;
                    case BLorder.AddOrder:

                        int productId, orderId;
                        Console.WriteLine(@"Enter the productID number that you want to add");
                        while (!int.TryParse(Console.ReadLine(), out productId)) ;
                        Console.WriteLine(@"Enter the Order Id that you want to add");
                        while (!int.TryParse(Console.ReadLine(), out orderId)) ;

                        iblogic.Order.UpdateOIADD(orderId, productId);


                        break;
                    case BLorder.DeleteOrder:

                        int productId1, orderId1;
                        Console.WriteLine(@"Enter the productID number that you want to add");
                        while (!int.TryParse(Console.ReadLine(), out productId1)) ;
                        Console.WriteLine(@"Enter the Order Id that you want to add");
                        while (!int.TryParse(Console.ReadLine(), out orderId1)) ;

                        iblogic.Order.updateOIdelete(orderId1, productId1);


                        break;
                    case BLorder.ChangeAmount:
                        int productId2, orderId2, amount;
                        Console.WriteLine(@"Enter the productID number that you want to add");
                        while (!int.TryParse(Console.ReadLine(), out productId2)) ;
                        Console.WriteLine(@"Enter the Order Id that you want to add");
                        while (!int.TryParse(Console.ReadLine(), out orderId2)) ;
                        Console.WriteLine(@"Enter the amount that you want to add");
                        while (!int.TryParse(Console.ReadLine(), out amount)) ;

                        iblogic.Order.updateOIAmount(orderId2, productId2, amount);


                        break;
                    case BLorder.Exit:
                        break;
                    default:
                        throw new Exception("Unvalide choice press any key to continue...");

                }

            }




            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }




        } while (orderCode != BLorder.Exit);

    }
    static void CartMenue()
    {
        BO.Cart Tempcart;
        int id, amount;
        BLcart cartCode = new BLcart();
        do
        {
            Console.WriteLine(@"For Add Product To Cart tap 1");
            Console.WriteLine(@"For Update Ampunt Product tap 2");
            Console.WriteLine(@"For Confirm Order tap 3");
            Console.WriteLine(@"For return to menu tap 0 ");

            BLcart.TryParse(Console.ReadLine(), out cartCode);
            try
            {
                switch (cartCode)
                {
                    case BLcart.AddProductToCart:
                        Console.WriteLine(@"Enter the product ID number that you want to add");
                        while (!int.TryParse(Console.ReadLine(), out id)) ;
                        Tempcart = iblogic.Cart.Add(Maincart, id);//cart
                        Console.WriteLine(Tempcart);

                        break;
                    case BLcart.UpdateAmpuntProduct:
                        Console.WriteLine(@"Enter the product ID number that you want to update");
                        while (!int.TryParse(Console.ReadLine(), out id)) ;
                        Console.WriteLine(@"Enter the amount products that you want to update");
                        while (!int.TryParse(Console.ReadLine(), out amount)) ;
                        Tempcart = iblogic.Cart.Update(Maincart, id, amount);//cart
                        Console.WriteLine(Tempcart);

                        break;
                    case BLcart.ConfirmOrder:
                        string name, mail, address;
                        Console.WriteLine(@"Enter the name");
                        name = Console.ReadLine();
                        Console.WriteLine(@"Enter the mail");
                        mail = Console.ReadLine();
                        Console.WriteLine(@"Enter the adress");
                        address = Console.ReadLine();
                        Tuple<string?, string?, string?> tuple = new Tuple<string?, string?, string?>(name, mail, address);

                        iblogic.Cart.ConfirmOrder(Maincart, tuple);
                        break;
                    case BLcart.Exit:
                        break;
                    default:
                        throw new Exception("Unvalide choice press any key to continue...");
                        
                }

            }




            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }




        } while (cartCode != BLcart.Exit);

    }
}




