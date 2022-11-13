using DO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
/// <summary>
/// binyamin shapira:208965863
/// oz asban:207565607
/// file:DalProudct
/// discraption:
/// this file contain the class DalProudct
/// and is metouds.
/// this file is the connection beetwen the main to the Array of class Proudct
/// </summary>
internal class DalProduct:IProduct
{
    /// <summary>
    /// Add organ to the array and chack if id exitis
    /// </summary>
    /// <param name="p"></param>
    /// <returns>int</returns>
    public int? Add(Product p)
    {
        
        bool flag=false;
        foreach (var item in DataSource.LProduct)
        {
            if (item.ID == p.ID)
            {
                flag = true;
            }
        }
      
        if(!flag)
        {
            DataSource.Add(p);
            return p.ID;
        }
        else
        {
            throw new AllreadyExistException();
        }

    }
    
    public Product Get(int IDp)
    {
        foreach (var item in DataSource.LProduct)
        {
            if(item.ID==IDp)
            {
                return item;    
            }

        }
        throw new NotFoundException();
    }

    public void Delete(int id)
    {
        foreach (var item in DataSource.LProduct)
        {
            if (item.ID==id)
            {
                DataSource.LProduct.Remove(item);
                return;
            }
        }
        throw new NotFoundException();
    }
    /// <summary>
    /// return new array of all product
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product> GetAll()
    {
       List<Product> products = new List<Product>(DataSource.LProduct);

        return products;

    }
    /// <summary>
    /// update the product
    /// </summary>
   
    public void Update(Product p)
    {
        foreach (var item in DataSource.LProduct)
        { 
            if (item.ID==p.ID)
            {
                DataSource.LProduct[DataSource.LProduct.IndexOf(item)] = p;
            }
        }
        throw new NotFoundException();


    }
  

}
