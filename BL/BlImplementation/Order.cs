using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    private IDal Dal = new DalList();

    public BO.Order Get(int id)
    {
        Dal.Order.Get(id);

        throw new NotImplementedException();
    }

    public IEnumerable<BO.OrderForList> GetList()
    {
        IEnumerable<DO.Order> Oldlist = Dal.Order.GetAll();

        List<BO.OrderForList> NewList = new List<BO.OrderForList>();

        foreach (var item in Oldlist)
        {
            BO.OrderForList OFL = new BO.OrderForList();

            OFL.ID = item.ID;
            OFL.CustomerName = item.CustomerName;



        }

        throw new NotImplementedException();
    }

    public OrderTracking OrderTracking(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Order UpdateDelivery(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Order UpdateOrder(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Order UpdateShip(int id)
    {
        throw new NotImplementedException();
    }
}
