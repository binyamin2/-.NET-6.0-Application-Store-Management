using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;

public struct Product
{
    public int ID { get; set; }
    public string Name { get; set; }
    
    public double Price { get; set; }


    public Catagory Catagory { get; set; }


    public int InStock { get; set; }
}
