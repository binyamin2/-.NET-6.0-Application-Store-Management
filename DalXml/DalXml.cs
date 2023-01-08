using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;
using System.Diagnostics;

sealed internal class DalXml : IDal
{
    private DalXml() { }
    static DalXml() { }

    public static IDal Instance { get; } = new DalXml();
    public IProduct Product { get; } = new Dal.Product();   

    public IOrder Order { get; } = new Dal.Order();

    public IOrderItem OrderItem { get; } = new Dal.OrderItem();
}
