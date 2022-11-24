using Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
/// <summary>
/// for the screen of orderitems list and details of order screen
/// </summary>
public class OrderItem
{
    private IDal Dal = new DalList();
    public int ID { get; set; }
    public int ProdectID { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
    ID : {ID}
    Name : {Name}
    Product ID :{ProdectID}
    Price: {Price}
    Amount in order: {Amount}
    TotalPrice : {TotalPrice}";

 



    }
}
