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
    public int Add(Product p)
    {
        
        bool flag=false;
        foreach (var item in DataSource.LProduct)
        {
            if (item?.ID == p.ID)
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
            throw new AllreadyExistException("the item is allready exist");
        }

    }
    
    public Product? Get(int IDp)
    {
        foreach (var item in DataSource.LProduct)
        {
            if(item?.ID==IDp)
            {
                return item;    
            }

        }
        throw new NotFoundException("the item not found");
    }
    /// <summary>
    /// delete product from list
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="NotFoundException"></exception>
    public void Delete(int id)
    {

        DO.Product? check = DataSource.LProduct.Find(i => i?.ID == id);
        if(check == null)
            throw new NotFoundException("the product not found");
        DataSource.LProduct= DataSource.LProduct.Where(x => x?.ID!=id).ToList();
    }
    /// <summary>
    /// return new array of all product
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product?> GetAll(Func<DO.Product?, bool>? predict = null)
    {
        if( predict == null)
        { 
            List<Product?> products = new List<Product?>(DataSource.LProduct);

            return products;
        }
        else
        {
            List<Product?> listProduct = (from Product in DataSource.LProduct
                                          where predict(Product) == true
                                          orderby Product?.ID 
                                          select Product).ToList();
            return listProduct;
        }

    }
    /// <summary>
    /// update the product
    /// </summary>
   
    public void Update(Product p)
    {
        
        foreach (var item in DataSource.LProduct.ToList())
        { 
            if (item?.ID==p.ID)
            {
                DataSource.LProduct[DataSource.LProduct.IndexOf(item)] = p;
                return;
            }
        }
         throw new NotFoundException("the product not found");
        

    }
    /// <summary>
    /// get product from list according to some condition
    /// </summary>
    /// <param name="predict"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public Product? Get(Func<Product?, bool>? predict)
    {

        DO.Product? check = DataSource.LProduct.Find(i => predict(i));
        if (check == null)
            throw new NotFoundException("the item not found");
        return check;
    }
}
