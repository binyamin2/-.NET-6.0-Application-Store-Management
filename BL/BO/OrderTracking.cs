using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BO;

public class OrderTracking
{
    public int? ID { get; set; }
    
    public OrderStatus? Status { get; set; }

    public List<Tuple<DateTime?, String>> DateList { get; set; }

    public override string ToString() => $@"
        Product ID :{ID}
        OrderStatus : {Status}";
}
