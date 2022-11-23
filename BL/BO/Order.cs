using Dal;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Order
{
    public int? ID { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAdress { get; set; }

    public OrderStatus Status { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryDate { get; set; }

    public DateTime? PaymentDate { get; set; }

    public List<OrderItem?> Items { get; set; }

    public double? TotalPrice { get; set; }

    public override string ToString() => $@"
        Order ID : {ID}
        Name :{CustomerName} 
        Adress : {CustomerAdress}
        Mail : {CustomerEmail}
        OrderStatus {Status}
    	OrderDate: {OrderDate}
        PaymentDate: {PaymentDate}
    	ShipDate: {ShipDate}
        DeliveryDate: {DeliveryDate}
        TotalPrice : {TotalPrice}
        
";
    private IDal Dal = new DalList();

    public Order()
    { }

    public Order(DO.Order order)
    {


        this.ID = order.ID;
        this.CustomerName = order.CustomerName;
        this.CustomerAdress = order.CustomerAdress;
        this.CustomerEmail = order.CustomerEmail;
        this.OrderDate = order.OrderDate;
        this.ShipDate = order.ShipDate;
        this.DeliveryDate = order.DeliveryDate;
        this.PaymentDate = this.OrderDate;

        if (order.DeliveryDate != null)
            this.Status = BO.OrderStatus.Deliverd;
        else if (order.ShipDate != null)
            this.Status = BO.OrderStatus.Shiped;
        else
            this.Status = BO.OrderStatus.Confirmed;

        ///list of orderitem
        IEnumerable<DO.OrderItem> ListOrderItem = Dal.OrderItem.GetAll();


        ///HelpOrder , it need for method "CalcAmountAndTotal"
        BlImplementation.Order HelpOrder = new BlImplementation.Order();

        var tuple = HelpOrder.CalcAmountAndTotal(this.ID, ListOrderItem);

        this.TotalPrice = tuple.Item2;

        ///build the list with "linq" and constractor of BO.OrderItem
        this.Items =
                (from OrderItem in ListOrderItem
                 where OrderItem.OrderID == this.ID
                 select new BO.OrderItem(OrderItem)).ToList();

        
    }

}
