using DO;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public void deleteOrderItem(int id)
    {

    }

}


