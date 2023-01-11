using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
internal sealed class DalXml : IDal
{
    private DalXml() { }
    static DalXml() { }

    public static IDal Instance { get; } = new DalXml();
    public IProduct Product { get; } = new DalProduct();

    public IOrder Order { get; } = new DalOrder();

    public IOrderItem OrderItem { get; } = new DalOrderItem();
}
