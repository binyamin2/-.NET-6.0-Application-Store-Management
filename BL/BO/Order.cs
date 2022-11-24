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
    public int ID { get; set; }
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
 

 

    

}
