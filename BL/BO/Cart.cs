using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Cart
{
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAdress { get; set; }
    public List<OrderItem?>? Items { get; set; } = new List<OrderItem?>();

    public double? TotalPrice { get; set; } = 0;


    public override string ToString() => $@"
        Name :{CustomerName} 
        Adress : {CustomerAdress}
        Mail : {CustomerEmail}
        TotalPrice : {TotalPrice}
        
";
}
