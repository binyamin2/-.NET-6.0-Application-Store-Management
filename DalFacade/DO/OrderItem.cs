using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DO;

public struct OrderItem
{
    /// <summary>
    /// OrderItem data
    /// </summary>
    public int OrderItemID { get; set; }
    public int ProdectID { get; set; }//s

    public int OrderID { get; set; }

    public double Price { get; set; }
    public int Amount { get; set; }


    /// <summary>
    /// override string ToString
    /// </summary>
    public override string ToString() => $@"
    Product ID={ProdectID}:
    OrderID - {OrderID}
    Price: {Price}
    Amount in order: {Amount}
";

}
