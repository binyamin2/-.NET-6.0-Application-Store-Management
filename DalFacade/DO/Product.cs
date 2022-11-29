using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DO;
/// <summary>
/// binyamin shapira:208965863
/// oz asban:207565607
/// file:Proudct
/// discraption:
/// this file is class of "Proudct"
/// </summary>
public struct Product
{
    /// <summary>
    /// product data
    /// </summary>
    public int ID { get; set; }
    public string? Name { get; set; }
    
    public double Price { get; set; }


    public Category? Category { get; set; }


    public int InStock { get; set; }


    /// <summary>
    /// override tostring
    /// </summary>
    public override string ToString() => $@"
        Product ID :{ID}
        Product name :{Name} 
        category :{Category}
    	Price: {Price}
    	Amount in stock: {InStock}";

}

