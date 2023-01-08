using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;

internal class Order : IOrder
{
    public int Add(DO.Order value)
    {
        throw new NotImplementedException();
    }

    public void Delete(int value)
    {
        throw new NotImplementedException();
    }

    public DO.Order? Get(int id)
    {
        throw new NotImplementedException();
    }

    public DO.Order? Get(Func<DO.Order?, bool>? predict)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? predict = null)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Order value)
    {
        throw new NotImplementedException();
    }
}
