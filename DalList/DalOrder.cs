using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DO;

namespace Dal;

/// <summary>
/// binyamin shapira:208965863
/// oz asban:207565607
/// file:DalOrder
/// discraption:
/// this file contain the class DalOrder
/// and is metouds.
/// this file is the connection beetwen the main to the Array of class Order
/// </summary>
public class DalOrder
{
    /// <summary>
    /// Add organ to the array and give it id
    /// </summary>
    /// <param name="order"></param>
    /// <returns>int?</returns>
    public int? Add(Order order)
    {

        order.ID = DataSource.IDOrder;

        DataSource.getIDOrder();

        DataSource.AddOrder(order);

        return order.ID;
    }

    public Order getOrder(int IDorder)
    {
        for (int i = 0; i < DataSource.OrderIndex; i++)
        {
            if (DataSource.LOrder[i].ID == IDorder)
            {
                return DataSource.LOrder[i];
            }
        }
        Order order = new Order();
        order.ID = -1;
        return order;
    }
    /// <summary>
    /// return new array of all product
    /// </summary>
    /// <returns></returns>
    public Order[] GetAllorder ()
    {
        Order[] order = new Order[DataSource.LOrder.Length];

        DataSource.LOrder.CopyTo(order, 0);

        return order;

    }
    /// <summary>
    /// update the product
    /// </summary>
    public void UpdateArOrder(Order order )
    {
        for (int i = 0; i < DataSource.OrderIndex; i++)
        {
            if (DataSource.LOrder[i].ID == order.ID)
            {
               DataSource.LOrder[i] = order;
                return;
            }
        }
        throw new Exception("the object not found");
      
    }

    public void deleteOrder(int id)
    {
        for (int i = 0; i < DataSource.OrderIndex; i++)
        {
            if (DataSource.LOrder[i].ID == id)
            {
                for (int j = i; j < DataSource.OrderIndex - 1; j++)
                {
                    DataSource.LOrder[j] = DataSource.LOrder[j + 1];
                }
                DataSource.OrderIndex--;
                return;
            }
        }
    }
    /// <summary>
    /// return current index
    /// </summary>
    /// <returns></returns>
    public int Get_current_index() { return DataSource.OrderIndex; }


}

