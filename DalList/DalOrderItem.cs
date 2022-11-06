using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

public class DalOrderItem
{
    public int AddOrderItem(OrderItem OI) 
    {
        OI.OrderItemID = DataSource.IDOrderItem;

        DataSource.getID_OI();

        DataSource.AddOrderItem(OI);
         
        return OI.OrderItemID;
       
    }

    public OrderItem getOrderItem(int IDorderItem)
    {
        for (int i = 0; i < DataSource.OrderIndex; i++)
        {
            if (DataSource.ArrOrderItem[i].OrderItemID == IDorderItem)
            {
                return DataSource.ArrOrderItem[i];
            }
        }
        OrderItem orderItem = new OrderItem();
        orderItem.OrderItemID = -1;
        return orderItem;
    }

    public void deleteOrderItem(int id)
    {
        for(int i = 0; i < DataSource.OrderItemIndex; i++)
        {
            if(DataSource.ArrOrderItem[i].OrderItemID == id)
            {
                for (int j = i; j < DataSource.OrderItemIndex - 1; j++)
                {
                    DataSource.ArrOrderItem[j] = DataSource.ArrOrderItem[j+1];
                }
                DataSource.OrderItemIndex--;
            }
        }
    }

    public OrderItem[] GetAllOrderItem()
    {
        OrderItem[] orderItems = new OrderItem[DataSource.ArrOrderItem.Length];

        DataSource.ArrOrderItem.CopyTo(orderItems, 0);

        return orderItems;

    }

    public void UpdateArOrderItem(OrderItem orderItem)
    {
        for (int i = 0; i < DataSource.OrderIndex; i++)
        {
            if (DataSource.ArrOrderItem[i].OrderItemID == orderItem.OrderItemID)
            {
                DataSource.ArrOrderItem[i] = orderItem;
                return;
            }
        }
        throw new Exception("the object not found");

    }
    public OrderItem GetItemByOrderAndProduct(int orderId,int productId)
    {
        foreach (OrderItem item in DataSource.ArrOrderItem)
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
        OrderItem[] tempArray = new OrderItem[DataSource.OrderItemIndex];
        int index1 = 0;
        foreach (OrderItem item in DataSource.ArrOrderItem)
        {
            if (item.OrderID == orderId)
            {
                tempArray[index1] = item;
                index1++;
            }
        }

        throw new Exception("the object not found");
    }
}


