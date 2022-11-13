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
public class DalOrderItem
{
    /// <summary>
    /// Add organ to the array and give it id
    /// </summary>
    /// <param name="OI"></param>
    /// <returns>int</returns>
    public int AddOrderItem(OrderItem OI) 
    {
        OI.OrderItemID = DataSource.IDOrderItem;

        DataSource.getID_OI();

        DataSource.AddOrderItem(OI);
         
        return OI.OrderItemID;
       
    }
    /// <summary>
    /// get orderitem by id
    /// </summary>
    /// <param name="IDorderItem"></param>
    public OrderItem getOrderItem(int IDorderItem)
    {
        for (int i = 0; i < DataSource.OrderItemIndex; i++)
        {
            if (DataSource.LOrderItem[i].OrderItemID == IDorderItem)
            {
                return DataSource.LOrderItem[i];
            }
        }
        throw new Exception("the object not found");
    }
    /// <summary>
    /// delete organ
    /// </summary>
    /// <param name="id"></param>
    public void deleteOrderItem(int id)
    {
        for(int i = 0; i < DataSource.OrderItemIndex; i++)
        {
            if(DataSource.LOrderItem[i].OrderItemID == id)
            {
                for (int j = i; j < DataSource.OrderItemIndex - 1; j++)
                {
                    DataSource.LOrderItem[j] = DataSource.LOrderItem[j+1];
                }
                DataSource.OrderItemIndex--;
            }
        }
    }
    /// <summary>
    /// return new array of all OrderItem
    /// </summary>
    /// <returns></returns>
    public OrderItem[] GetAllOrderItem()
    {
        OrderItem[] orderItems = new OrderItem[DataSource.LOrderItem.Length];

        DataSource.LOrderItem.CopyTo(orderItems, 0);

        return orderItems;

    }
    /// <summary>
    /// update the product
    /// </summary>

    public void UpdateArOrderItem(OrderItem orderItem)
    {
        for (int i = 0; i < DataSource.OrderItemIndex; i++)
        {
            if (DataSource.LOrderItem[i].OrderItemID == orderItem.OrderItemID)
            {
                DataSource.LOrderItem[i] = orderItem;
                return;
            }
        }
        throw new Exception("the object not found");

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

        throw new Exception("the object not found");
    }

    public OrderItem[] GetItemsListByOrderId(int orderId)
    {
        
        int index1 = 0;
        bool flag = false;///if have minimum one organ in new array
        foreach (OrderItem item in DataSource.LOrderItem)
        {
            if (item.OrderID == orderId)
            {
                index1++;
                flag = true;

            }
        }
        if (!flag)///not found organ
        {
            throw new Exception("the object not found");
        }
        OrderItem[] tempArray = new OrderItem[index1];///length of new array acorrding num of organ that found

        index1 = 0;
        foreach (OrderItem item in DataSource.LOrderItem)
        {
            if (item.OrderID == orderId)
            {
                tempArray[index1] = item;
                index1++;
            }
        }

        return tempArray;
        
    }
    /// <summary>
    /// return current index
    /// </summary>
    /// <returns></returns>
    public int Get_current_index() { return DataSource.OrderItemIndex; }
}


