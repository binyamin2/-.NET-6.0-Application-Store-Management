using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DO;
using DalApi;

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
internal class DalOrder:IOrder
{
    /// <summary>
    /// Add organ to the array and give it id
    /// </summary>
    /// <param name="order"></param>
    /// <returns>int?</returns>
    public int Add(Order order)
    {

        order.ID = DataSource.IDOrder;

        DataSource.getIDOrder();

        DataSource.AddOrder(order);

        return order.ID;
    }

    public Order Get(int IDorder)
    {
       
            foreach (var order in DataSource.LOrder)
            {
                if (order.ID == IDorder)
                {
                    return order;
                }
            }
            throw new NotFoundException("the item not found");



    }

    /// <summary>
    /// return new array of all product
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order> GetAll ()
    {
        List<Order> listOrder = new List<Order>(DataSource.LOrder);

        return listOrder;

    }
    /// <summary>
    /// update the product
    /// </summary>
    public void Update(Order order )
    {
        foreach (var organ in DataSource.LOrder)
        {
            if (organ.ID == order.ID)
            {
                DataSource.LOrder[DataSource.LOrder.IndexOf(organ)] = order;
              
                return;
            }
        }
        throw new NotFoundException("the item not found");
      
    }

    public void Delete(int id)
    {
        foreach (var item in DataSource.LOrder)
        {
            if (item.ID == id )
            {
                DataSource.LOrder.Remove(item);
            }
        }
        throw new NotFoundException("the item not found");
    }
    /// <summary>
    /// return current index
    /// </summary>
    /// <returns></returns>



}

