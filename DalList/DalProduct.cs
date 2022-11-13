using DO;
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
public class DalProduct
{
    /// <summary>
    /// Add organ to the array and chack if id exitis
    /// </summary>
    /// <param name="p"></param>
    /// <returns>int</returns>
    public int AddProduct(Product p)
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
            DataSource.AddProduct(p);
            return p.ID;
        }
        else
        {
            throw new Exception("the ID exitis");
        }

    }
    
    public Product getPruduct(int IDp)
    {
        foreach (var item in DataSource.LProduct)
        {
            if(item.ID==IDp)
            {
                return item;    
            }

        }
        throw new Exception("the product not found");
    }

    public void deleteProduct(int id)
    {
        foreach (var item in DataSource.LProduct)
        {
            if (item.ID==id)
            {
                DataSource.LProduct.Remove(item);
                return;
            }
        }
        throw new Exception("the product not find");
    }
    /// <summary>
    /// return new array of all product
    /// </summary>
    /// <returns></returns>
    public List<Product> GetAllProduct()
    {
       List<Product> products = new List<Product>(DataSource.LProduct);

        return products;

    }
    /// <summary>
    /// update the product
    /// </summary>
   
    public void UpdateProduct(Product p)
    {
        foreach (var item in DataSource.LProduct)
        { 
            if (item.ID==p.ID)
            {
                DataSource.LProduct[DataSource.LProduct.IndexOf(item)] = p;
            }
        }
        throw new Exception("the Product not find");
       

    }
  

}
