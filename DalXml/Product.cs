namespace Dal;
using  DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Security.Principal;

internal class Product : IProduct
{
    public int Add(DO.Product value)
    {
        throw new NotImplementedException();
    }

    public void Delete(int value)
    {
        throw new NotImplementedException();
    }

    public DO.Product? Get(int id)
    {
        throw new NotImplementedException();
    }

    public DO.Product? Get(Func<DO.Product?, bool>? predict)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? predict = null)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Product value)
    {
        throw new NotImplementedException();
    }
}