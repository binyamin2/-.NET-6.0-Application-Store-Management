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

    private IDal Dal = new DalList();

    OrderItem()
    { }
    OrderItem(DO.OrderItem orderItem)
    {
        this.ID = orderItem.OrderItemID;
        this.ProdectID = orderItem.ProdectID;
        this.Price = orderItem.Price;
        this.Amount = orderItem.Amount;
        this.TotalPrice = orderItem.Price * orderItem.Amount;
        private IDal Dal = new DalList();

        ///find the name of the product

        IEnumerable<DO.Product> ListOrderItem = Dal.Product.GetAll();



        foreach (var item in ListOrderItem)
        {
            if (this.ID == item.ID)
            {
                this.Name = item.Name;
                return;
            }



    }



    }
}
