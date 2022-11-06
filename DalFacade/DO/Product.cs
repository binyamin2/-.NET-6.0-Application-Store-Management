using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DO;

public struct Product
{
    /// <summary>
    /// product data
    /// </summary>
    public int ID { get; set; }
    public string Name { get; set; }
    
    public double Price { get; set; }


    public Category Category { get; set; }


    public int InStock { get; set; }


    /// <summary>
    /// override tostring
    /// </summary>
    public override string ToString() => $@"
        Product ID={ID}, {Name} 
        category :{Category}
    	Price: {Price}
    	Amount in stock: {InStock}";

}

