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
        OI.ID = DataSource.IDOrderItem;

        DataSource.getID_OI();

        DataSource.Add(OI);
         
        return OI.ID;
       
    }
    /// <summary>
    /// get orderitem by id
    /// </summary>
    /// <param name="IDorderItem"></param>
    public OrderItem? Get(int IDorderItem)
    {

        DO.OrderItem? check = DataSource.LOrderItem.Find(i => i?.ID==IDorderItem);
        if (check == null)
            throw new NotFoundException("the orderitem not found");
        return check;
    }
    /// <summary>
    /// delete organ
    /// </summary>
    /// <param name="id"></param>
    public void Delete(int id)
    {
        DO.OrderItem? check = DataSource.LOrderItem.Find(i => i?.ID == id);
        if (check == null)
            throw new NotFoundException("the orderitem not found");
        DataSource.LOrderItem = DataSource.LOrderItem.Where(x => x?.ID != id).ToList();
    }
    /// <summary>
    /// return new array of all OrderItem
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? predict = null)
    {
        if (predict == null)
        {
            List<OrderItem?> listOrderItem = new List<OrderItem?>(DataSource.LOrderItem);
            return listOrderItem;
        }

        else
        {
            List<OrderItem?> listOrderItem = (from OrderItem in DataSource.LOrderItem
                                     where predict(OrderItem) == true
                                     select OrderItem).ToList();
            return listOrderItem;
        }
    }
    /// <summary>
    /// update the product
    /// </summary>

    public void Update(OrderItem orderItem)
    {
        foreach (var item in DataSource.LOrderItem.ToList())
        {
            if (item?.ID == orderItem.ID)
            {
                DataSource.LOrderItem[DataSource.LOrderItem.IndexOf(item)] = orderItem;
                return;
            }
        }
        throw new NotFoundException("the orderitem not found");

    }
    /// <summary>
    /// return order items according to order id and product id
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public OrderItem? GetItemByOrderAndProduct(int orderId,int productId)
    {
        
        DO.OrderItem? check = DataSource.LOrderItem.Find(i => i?.OrderID ==orderId&&i?.ProdectID==productId );
        if (check == null)
            throw new NotFoundException("the orderitem not found");
        return check;
    }
    /// <summary>
    /// return order items according to order id
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public IEnumerable<OrderItem?> GetItemsListByOrderId(int orderId)
    {
       
        DO.OrderItem? check = DataSource.LOrderItem.Find(i => i?.ID == orderId);
        if (check == null)
            throw new NotFoundException("the orderitem not found");
        return DataSource.LOrderItem.Where(item => item?.OrderID == orderId);
    }
    /// <summary>
    /// get order item from list according to some condition
    /// </summary>
    /// <param name="predict"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public OrderItem? Get(Func<OrderItem?, bool>? predict)
    {
 
        DO.OrderItem? check = DataSource.LOrderItem.Find(i => predict(i));
        if (check == null)
            throw new NotFoundException("the orderitem not found");
        return check;
    }

 


}


