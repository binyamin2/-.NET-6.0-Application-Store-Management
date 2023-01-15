using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace Dal;

 internal sealed class DalList : IDal
{
    private DalList() { }
    static DalList() { }
   
    public static IDal Instance { get; } = new DalList();

    public IProduct Product { get; } =  new DalProduct();

    public IOrder Order { get; } = new DalOrder();

    public IOrderItem OrderItem { get; } = new DalOrderItem();
    
}
