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
        OI.OrderItemID = DataSource.Config.IDOrderItem;

        DataSource.Config.getID_OI();

        DataSource.AddOrderItem(OI);
         
        return OI.OrderItemID;
       
    }

    public OrderItem getOrderItem(int IDorderItem)
    {
        for (int i = 0; i < DataSource.Config.OrderIndex; i++)
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
        for(int i = 0; i < DataSource.ArrOrderItem.Length; i++)
        {
            if(DataSource.ArrOrderItem[i].OrderItemID == id)
            {
                for (int j = i; j < DataSource.ArrOrderItem.Length - 1; j++)
                {
                    DataSource.ArrOrderItem[j] = DataSource.ArrOrderItem[j+1];
                }
                DataSource.Config.OrderItemIndex--;
            }
        }
    }

}


