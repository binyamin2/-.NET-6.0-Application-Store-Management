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
   
    private DataSource? ds = DataSource.Instance;

    public int? Add(Order order)
    {

        order.ID = DataSource.IDOrder;

        ds.getIDOrder();

        ds.AddOrder(order);

        return order.ID;
    }

    public Order Get(int IDorder)
    {
       
            foreach (var order in ds.LOrder)
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
        List<Order> listOrder = new List<Order>(ds.LOrder);

        return listOrder;

    }
    /// <summary>
    /// update the product
    /// </summary>
    public void Update(Order order )
    {
        foreach (var organ in ds.LOrder)
        {
            if (organ.ID == order.ID)
            {
                ds.LOrder[ds.LOrder.IndexOf(organ)] = order;
              
                return;
            }
        }
        throw new NotFoundException("the item not found");
      
    }

    public void Delete(int id)
    {
        foreach (var item in ds.LOrder)
        {
            if (item.ID == id )
            {
                ds.LOrder.Remove(item);
            }
        }
        throw new NotFoundException("the item not found");
    }
    /// <summary>
    /// return current index
    /// </summary>
    /// <returns></returns>



}

