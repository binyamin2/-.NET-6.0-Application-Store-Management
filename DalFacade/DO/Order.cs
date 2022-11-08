using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DO;

public struct Order
{
    /// <summary>
    /// order data
    /// </summary>
    public int? ID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdress { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ShipDate { get; set; } 
    public DateTime? DeliveryDate { get; set; }
    /// <summary
    /// override tostring
    /// </summary>
   
    public override string ToString() => $@"
        Order ID : {ID}
        Name :{CustomerName} 
        Adress : {CustomerAdress}
    	OrderDate: {OrderDate}
    	ShipDate: {ShipDate}
        DeliveryDate: {DeliveryDate}
";


}
