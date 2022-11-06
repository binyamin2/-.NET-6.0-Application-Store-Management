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

        order.ID = DataSource.Config.IDOrder;

        DataSource.Config.getIDOrder();

        DataSource.AddOrder(order);

        return order.ID;
    }

    public Order getOrder(int IDorder)
    {
        for (int i = 0; i < DataSource.Config.OrderIndex; i++)
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
    public 


}

