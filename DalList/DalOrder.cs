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

    public Order? Get(int IDorder)
    {
        DO.Order? check = DataSource.LOrder.Find(i => i?.ID==IDorder);
        if (check == null)
            throw new NotFoundException("the Order not found");
        return check;
    }

    /// <summary>
    /// return new array of all product
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order?> GetAll (Func<Order?, bool>? predict = null)
    {
        if (predict == null)
        {
            List<Order?> listOrder = new List<Order?>(DataSource.LOrder);
            return listOrder;
        }
        else
        {
            List<Order?> listOrder = (from Order in DataSource.LOrder
                                     let flag = true
                                     where predict(Order) == flag
                                     select Order).ToList();
            return listOrder;
        }

        

    }


    /// <summary>
    /// update the product
    /// </summary>
    public void Update(Order order )
    {
        foreach (var organ in DataSource.LOrder)
        {
            if (organ?.ID == order.ID)
            {
                DataSource.LOrder[DataSource.LOrder.IndexOf(organ)] = order;
              
                return;
            }
        }
        throw new NotFoundException("the item not found");
      
    }

    public void Delete(int id)
    {

        DO.Order? check = DataSource.LOrder.Find(i => i?.ID == id);
        if (check == null)
            throw new NotFoundException("the Order not found");
        DataSource.LOrder = DataSource.LOrder.Where(x => x?.ID != id).ToList();
    }

    public Order? Get(Func<Order?, bool>? predict)
    {

        DO.Order? check = DataSource.LOrder.Find(i => predict(i));
        if (check == null)
            throw new NotFoundException("the Order not found");
        return check;
    }

    /// <summary>
    /// return current index
    /// </summary>
    /// <returns></returns>



}

