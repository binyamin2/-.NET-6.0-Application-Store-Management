using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using Dal;
using DalApi;
using DO;

namespace BlImplementation;

internal class Product : BlApi.IProduct
{
    private IDal Dal = new DalList();
    
    public void Add(BO.Product product)
    {
        if (product.ID < 100000 || product.ID > 999999)
            throw new Exception("id not valid");
        if (product.Name == null)
            throw new Exception("name not valid");
        if (product.Price < 0)
            throw new Exception("price not valid");
        if (product.InStock < 0)
            throw new Exception("InStock not valid");
        DO.Product p_DO = new DO.Product();
        p_DO.ID = product.ID;
        p_DO.Name = product.Name;
        p_DO.Price = product.Price;
        p_DO.InStock = product.InStock;
        p_DO.Category = (DO.Category)product.Category;
        Dal.Product.Add(p_DO);
    
    }

    public void Delete(int? id)
    {
        for
        throw new NotImplementedException();
    }

    public BO.Product GetForClient(int? id, BO.Cart cart)
    {
        throw new NotImplementedException();
    }

    public BO.Product GetForManager(int? id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Product> GetList()
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Product product)
    {
        throw new NotImplementedException();
    }
}
