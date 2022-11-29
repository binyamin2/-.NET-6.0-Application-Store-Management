using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public  class ProductItem
{

    public int Amount { get; set; }

    public int ID { get; set; }
    public string? Name { get; set; }
    public double? Price { get; set; }
    public Category? Category { get; set; }
    public bool InStock { get; set; }

    public override string ToString() => $@"
        Product ID :{ID}
        Product name :{Name} 
        category :{Category}
    	Price: {Price}
    	Amount: {Amount}
        InStock: {InStock}
";
        
}
