using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
/// <summary>
/// binyamin shapira:208965863
/// oz asban:207565607
/// file:DalOrderItem
/// discraption:
/// this file contain the class DalOrderItem
/// and is metouds.
/// this file is the connection beetwen the main to the Array of class OrderItem
/// </summary>
internal class DalOrderItem :IOrderItem
{
    /// <summary>
    /// Add organ to the array and give it id
    /// </summary>
    /// <param name="OI"></param>
    /// <returns>int</returns>
  public int Add(OrderItem OI) 
    {
        OI.OrderItemID = DataSource.IDOrderItem;

        DataSource.getID_OI();

        DataSource.Add(OI);
         
        return OI.OrderItemID;
       
    }
    /// <summary>
    /// get orderitem by id
    /// </summary>
    /// <param name="IDorderItem"></param>
    public OrderItem Get(int IDorderItem)
    {
        foreach (var organ in DataSource.LOrderItem)
        {
            if (organ.OrderItemID == IDorderItem)
            {
                return organ;
            }
        }
        throw new NotFoundException("the item not found");
    }
    /// <summary>
    /// delete organ
    /// </summary>
    /// <param name="id"></param>
    public void Delete(int id)
    {
        foreach (var item in DataSource.LOrderItem)
        {
            if (item.OrderItemID == id)
            {
                DataSource.LOrderItem.Remove(item);
            }
        }
        throw new NotFoundException("the item not found");
    }
    /// <summary>
    /// return new array of all OrderItem
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderItem> GetAll()
    {
        List<OrderItem> listOrderItem = new List<OrderItem>(DataSource.LOrderItem);

        return listOrderItem;

    }
    /// <summary>
    /// update the product
    /// </summary>

    public void Update(OrderItem orderItem)
    {
        foreach (var item in DataSource.LOrderItem)
        {
            if (item.OrderItemID == orderItem.OrderItemID)
            {
                DataSource.LOrderItem[DataSource.LOrderItem.IndexOf(item)] = orderItem;
            }
        }
        throw new NotFoundException("the item not found");

    }
    public OrderItem GetItemByOrderAndProduct(int orderId,int productId)
    {
        foreach (OrderItem item in DataSource.LOrderItem)
        {
            if (item.OrderID ==orderId && item.ProdectID == productId)
            {
                return item;
            }
        }

        throw new NotFoundException("the item not found");
    }

    public IEnumerable<OrderItem> GetItemsListByOrderId(int orderId)
    {
        List<OrderItem> LOI = new List<OrderItem> ();
        bool flag = false;///if have minimum one organ in new array
        foreach (OrderItem item in DataSource.LOrderItem)
        {
            if (item.OrderID == orderId)
            {
                LOI.Add(item);
                flag = true;    
            }
        }
        if (!flag)///not found organ
        {
            throw new NotFoundException("the item not found");
        }
        return LOI;
        
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderItem"></param>
    /// <exception cref="NotFoundException"></exception>
 

}


