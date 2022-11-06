using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

public class DalProduct
{
    public int AddProduct(Product p)
    {
        bool flag=false;
        for (int i = 0; i < DataSource.ArrProduct.Length; i++)
        {
            if (DataSource.ArrProduct[i].ID==p.ID)
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
            return p.ID;
        }

    }

    public Product getPruduct(int IDp)
    {
        for (int i = 0; i < DataSource.ArrProduct.Length; i++)
        {
            if (DataSource.ArrProduct[i].ID == IDp)
            {
                return DataSource.ArrProduct[i];
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
            if (DataSource.ArrProduct[i].ID == id)
            {
                for (int j = i; j < DataSource.ProductIndex - 1; j++)
                {
                    DataSource.ArrProduct[j] = DataSource.ArrProduct[j + 1];
                }
                DataSource.ProductIndex--;
            }
        }
    }

    public Product[] GetAllProduct()
    {
        Product[] products = new Product[DataSource.ArrProduct.Length];

        DataSource.ArrProduct.CopyTo(products, 0);

        return products;

    }

    public void UpdateProduct(Product p)
    {
        for (int i = 0; i < DataSource.ProductIndex; i++)
        {
            if (DataSource.ArrProduct[i].ID == p.ID)
            {
                DataSource.ArrProduct[i] = p;
                return;
            }
        }
        throw new Exception("the object not found");

    }

}
