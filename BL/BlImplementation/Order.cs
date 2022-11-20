﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using Dal;
using DalApi;
using DO;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    private IDal Dal = new DalList();

    public BO.Order Get(int id)
    {
        if (0 >= id)
            throw new Exception("id not valid");

        ///try if id found in Orders list
        try
        {
            DO.Order order = Dal.Order.Get(id);

            BO.Order newOrder = new BO.Order();

            newOrder.ID = order.ID;
            newOrder.CustomerName = order.CustomerName;
            newOrder.CustomerAdress = order.CustomerAdress;
            newOrder.CustomerEmail = order.CustomerEmail;
            newOrder.OrderDate = order.OrderDate;
            newOrder.ShipDate = order.ShipDate;
            newOrder.DeliveryDate = order.DeliveryDate;

            if (order.DeliveryDate != null)
                newOrder.Status = OrderStatus.Deliverd;
            else if (order.ShipDate != null)
                newOrder.Status = OrderStatus.Shiped;
            else
                newOrder.Status = OrderStatus.Confirmed;

            IEnumerable<DO.OrderItem> ListOrderItem = Dal.OrderItem.GetAll();

            ///help metoud
            var tuple = CalcAmountAndTotal(newOrder.ID, ListOrderItem);

            newOrder.TotalPrice = tuple.Item2;

            newOrder.Items = ListOrderItem;///צריך להמיר מרשימה של order do לorder BO
            ///נראלי כדי לעשות פונקצייית עזר כזאת בorder item

            ///newOrder.PaymentDate = ?????


        }
        catch (Exception ex)
        {

            throw ex;
        }

       


        throw new NotImplementedException();
    }


    /// <summary>
    /// get list of "OrderForList" with calculate from data
    /// </summary>
    /// <returns>IEnumerable<BO.OrderForList></returns>
    public IEnumerable<BO.OrderForList> GetList()
    {   
        ///order list
        IEnumerable<DO.Order> Oldlist = Dal.Order.GetAll();

        ///new list 
        List<BO.OrderForList> NewList = new List<BO.OrderForList>();
        
        ///for update amount and total
        IEnumerable<DO.OrderItem> ListOrderItem = Dal.OrderItem.GetAll();

        foreach (var item in Oldlist)
        {
            ///update ID and name
            BO.OrderForList OrderFL = new BO.OrderForList();
            OrderFL.ID = item.ID;
            OrderFL.CustomerName = item.CustomerName;

            ///update the status enum
            if (item.DeliveryDate != null)
                OrderFL.Status = OrderStatus.Deliverd;
            else if (item.ShipDate != null)
                OrderFL.Status = OrderStatus.Shiped;
            else
                OrderFL.Status = OrderStatus.Confirmed;


            ///update the "Amount Items" and "Total price"
            int AmountItems = 0;
            Double TotalPrice = 0;

            foreach (var OrderItem in ListOrderItem)
            {
                if (OrderItem.OrderID == OrderFL.ID)
                {
                    AmountItems ++;
                    TotalPrice += (OrderItem.Amount * OrderItem.Price);
                }
            }
            OrderFL.TotalPrice = TotalPrice;
            OrderFL.AmountOfItems = AmountItems;

            ///add to new list
            NewList.Add(OrderFL);

        }
        return NewList;
       
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


    internal Tuple<int ,Double> CalcAmountAndTotal(int? id, IEnumerable<DO.OrderItem> orderItems)
    {
        int AmountItems = 0;
        Double TotalPrice = 0;

        foreach (var OrderItem in orderItems)
        {
            if (OrderItem.OrderID == id)
            {
                AmountItems++;
                TotalPrice += (OrderItem.Amount * OrderItem.Price);
            }
        }
        return Tuple.Create(AmountItems, TotalPrice);

    }
}
