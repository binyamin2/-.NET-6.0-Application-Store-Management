using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DO;

namespace Dal;

 public class DalOrder
{
    public int Add(Order order)
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
            if (DataSource.ArrOrder[i].ID == IDorder)
            {
                return DataSource.ArrOrder[i];
            }
        }
        Order order = new Order();
        order.ID = -1;
        return order;
    }
    public Order[] GetAllorder ()
    {
        Order[] order = new Order[DataSource.ArrOrder.Length];

        DataSource.ArrOrder.CopyTo(order, 0);

        return order;

    }

    public void UpdateArOrder(Order order )
    {
        for (int i = 0; i < DataSource.OrderIndex; i++)
        {
            if (DataSource.ArrOrder[i].ID == order.ID)
            {
               DataSource.ArrOrder[i] = order;
                return;
            }
        }
        throw new Exception("the object not found");
      
    }

    public void deleteOrder(int id)
    {
        for (int i = 0; i < DataSource.OrderIndex; i++)
        {
            if (DataSource.ArrOrder[i].ID == id)
            {
                for (int j = i; j < DataSource.OrderIndex - 1; j++)
                {
                    DataSource.ArrOrder[j] = DataSource.ArrOrder[j + 1];
                }
                DataSource.OrderIndex--;
                return;
            }
        }
    }


}

