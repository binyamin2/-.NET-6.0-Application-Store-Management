using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using Dal;

namespace BlTest;
enum BLoption { Cart = 1, Order, Product, Exit = 0 }
enum BLorder { GetListOrder = 1, GetOrder, UpdateShip, UpdateDelivery, TrackOrder, AddOrder, DeleteOrder, ChangeAmount, Exit = 0}

enum BLocart { AddProductToCart = 1, UpdateAmpuntProduct, ConfirmOrder, Exit =0 }

enum BLproduct { GetListProduct = 1, GetProduct, GetDetailsProduct, AddProduct, DeleteProduct, UpdateDetailsProduct,Exit =0 }
class MainBL
{
    BlApi.IBl IBL = new BlImplementation.Bl();

    private static BLoption menuCode;

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
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        } while (menuCode != BLoption.Exit);



        private static void ProductMenue()
        {



            do
            {
                Console.WriteLine(@"For see list a products tap 1");
                Console.WriteLine(@"For see product tap 2");
                Console.WriteLine(@"For see details product 3");
                Console.WriteLine(@"For add product tap 4");
                Console.WriteLine(@"For deletion product tap 5");
                Console.WriteLine(@"For update details product tap 6");
                Console.WriteLine(@"For return to menu press 0");

                BLproduct productCode;
                BLproduct.TryParse(Console.ReadLine(), out productCode);
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
                            InStock = tmpAmount
                        };
                        int? addedProductId = DalList.Product.Add(addedProduct);
                        Console.WriteLine("The Product id: {0}", addedProductId);
                        break;
                    case CraudMethod.View:
                        Console.WriteLine(@"Enter the product ID number that you want to see his details");
                        int.TryParse(Console.ReadLine(), out id);
                        Product productToShow = new Product();
                        productToShow = DalList.Product.Get(id);
                        Console.WriteLine(productToShow);
                        break;
                    case CraudMethod.ViewAll:
                        IEnumerable<Product> productsList = DalList.Product.GetAll();
                        foreach (var item in productsList)
                        {
                            Console.WriteLine(item);
                        }

                        break;
                    case CraudMethod.Update:
                        Console.WriteLine(@"Enter the product ID number that you want to update his details");
                        int.TryParse(Console.ReadLine(), out id);
                        Product oldProduct = DalList.Product.Get(id);
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
                            InStock = tmpAmount
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
            } while (menuCode != StoreItem.Exit);
            throw new NotImplementedException();
        }

        private static void OrderMenue()
        {
            throw new NotImplementedException();
        }

        private static void CartMenue()
        {
            throw new NotImplementedException();
        }
    }


}

