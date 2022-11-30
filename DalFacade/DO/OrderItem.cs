using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DO;

/// <summary>
/// binyamin shapira:208965863
/// oz asban:207565607
/// file:OrderItem
/// discraption:
/// this file is class of "OrderItem"
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// OrderItem data
    /// </summary>
    public int ID { get; set; }
    public int ProdectID { get; set; }//s

    public int OrderID { get; set; }

    public double Price { get; set; }
    public int Amount { get; set; }


    /// <summary>
    /// override string ToString
    /// </summary>
    public override string ToString() => $@"
    ID : {ID}
    Product ID :{ProdectID}
    OrderID : {OrderID}
    Price: {Price}
    Amount in order: {Amount}
";

}
