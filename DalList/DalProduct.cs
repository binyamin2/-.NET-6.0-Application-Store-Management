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
        for (int i = 0; i < DataSource.LProduct.Length; i++)
        {
            if (DataSource.LProduct[i].ID==p.ID)
            {
                flag=true;
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
        for (int i = 0; i < DataSource.LProduct.Length; i++)
        {
            if (DataSource.LProduct[i].ID == IDp)
            {
                return DataSource.LProduct[i];
            }
        }
        Product p = new Product();
        p.ID = -1;
        return p;
    }

    public void deleteProduct(int id)
    {
        for (int i = 0; i < DataSource.ProductIndex; i++)
        {
            if (DataSource.LProduct[i].ID == id)
            {
                for (int j = i; j < DataSource.ProductIndex - 1; j++)
                {
                    DataSource.LProduct[j] = DataSource.LProduct[j + 1];
                }
                DataSource.ProductIndex--;
            }
        }
    }
    /// <summary>
    /// return new array of all product
    /// </summary>
    /// <returns></returns>
    public Product[] GetAllProduct()
    {
        Product[] products = new Product[DataSource.LProduct.Length];

        DataSource.LProduct.CopyTo(products, 0);

        return products;

    }
    /// <summary>
    /// update the product
    /// </summary>
   
    public void UpdateProduct(Product p)
    {
        for (int i = 0; i < DataSource.ProductIndex; i++)
        {
            if (DataSource.LProduct[i].ID == p.ID)
            {
                DataSource.LProduct[i] = p;
                return;
            }
        }
        throw new Exception("the object not found");

    }
    /// <summary>
    /// return current index
    /// </summary>
    /// <returns></returns>
    public int Get_current_index() { return DataSource.ProductIndex; }

}
