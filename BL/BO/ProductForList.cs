using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
/// <summary>
/// for the screen of product list
/// </summary>
public class ProductForList
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public Category? Category { get; set; }


    public override string ToString() => $@"
        Product ID :{ID}
        Product name :{Name} 
        Price: {Price}
        category :{Category}";
}
