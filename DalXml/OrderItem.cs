using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;


internal class OrderItem : IOrderItem
{
    public int Add(DO.OrderItem value)
    {
        throw new NotImplementedException();
    }

    public void Delete(int value)
    {
        throw new NotImplementedException();
    }

    public DO.OrderItem? Get(int id)
    {
        throw new NotImplementedException();
    }

    public DO.OrderItem? Get(Func<DO.OrderItem?, bool>? predict)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? predict = null)
    {
        throw new NotImplementedException();
    }

    public DO.OrderItem? GetItemByOrderAndProduct(int orderId, int productId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.OrderItem?> GetItemsListByOrderId(int orderId)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.OrderItem value)
    {
        throw new NotImplementedException();
    }
}
