using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        if (0 >= id)
            throw new BO.InputUnvalidException("ID not valid");

        ///try if id found in Orders list
        try
        {
            DO.Order order = Dal.Order.Get(id);

            ///build the new organ with constractor of Order
            BO.Order NewOrder = BuildOrderBO(order);

            return NewOrder;


        }
        catch (Exception ex)
        {

            throw new BO.WorngOrderException(ex.Message, ex) ;
        }
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
            /////method help "CheckStatus"
            OrderFL.Status = CheckStatus(item);
           


            ///update the "Amount Items" and "Total price" with func help
            var tuple = CalcAmountAndTotal(OrderFL.ID, ListOrderItem);

            OrderFL.AmountOfItems = tuple.Item1;
            OrderFL.TotalPrice = tuple.Item2;

            ///add to new list
            NewList.Add(OrderFL);

        }
        return NewList;
       
    }

  

    public BO.OrderTracking OrderTracking(int id)
    {
        if (0 >= id)
        {
            throw new BO.InputUnvalidException("ID not valid");
        }

        try
        {
            ///build order and ordertracking
            DO.Order order = Dal.Order.Get(id);
            BO.OrderTracking orderTrack = new BO.OrderTracking();
            orderTrack.ID = order.ID;
            //method help "CheckStatus"
            orderTrack.Status = CheckStatus(order);
            ///if not order
            if (order.OrderDate != null)
            {
                Tuple<DateTime?, String> DateOrder = new Tuple<DateTime?, String>(order.OrderDate, "The order created");
                orderTrack.DateList.Add(DateOrder);
            }
            ///if not ship
            if (order.ShipDate != null)
            {
                Tuple<DateTime?, String> DateOrder = new Tuple<DateTime?, String>(order.ShipDate, "The order shiped");
                orderTrack.DateList.Add(DateOrder);
            }
            ///if not delivery
            if (order.DeliveryDate != null)
            {
                Tuple<DateTime?, String> DateOrder = new Tuple<DateTime?, String>(order.DeliveryDate, "The order delivered");
                orderTrack.DateList.Add(DateOrder);
            }

            return orderTrack;

        }
        catch (Exception ex)
        {

            throw new BO.WorngOrderException(ex.Message, ex);
        }
    }

    public BO.Order UpdateShip(int id)
    {
        if (0 >= id)
            throw new BO.InputUnvalidException("ID not valid");
        try
        {
            DO.Order order = Dal.Order.Get(id);
            //if allready ship
            if (order.ShipDate != null)
            {
                throw new BO.WorngOrderException("The order allready ship");
            }

            DateTime NowDate = DateTime.Now; 

            order.ShipDate = NowDate;

            //update the order n database
            Dal.Order.Update(order);

            ///create a BO.order and return it.
            BO.Order BOorder = BuildOrderBO(order);

            return BOorder;
        }
        catch (Exception ex)
        {

            throw new BO.WorngOrderException(ex.Message, ex);
        }
    }


    public BO.Order UpdateDelivery(int id)
    {
        if (0 >= id)
            throw new BO.InputUnvalidException("ID not valid");
        try
        {
            DO.Order order = Dal.Order.Get(id);
            //if not allready ship
            if (order.ShipDate == null)
            {
                throw new BO.WorngOrderException("The order not allready ship");
            }
            //if allready delivery
            if (order.DeliveryDate != null)
            {
                throw new BO.WorngOrderException("The order allready delivery");
            }

            DateTime NowDate = DateTime.Now;

            order.DeliveryDate = NowDate;

            //update the order in database
            Dal.Order.Update(order);

            ///create a BO.order and return it.
            BO.Order BOorder = BuildOrderBO(order);

            return BOorder;
        }
        catch (Exception ex)
        {

            throw new BO.WorngOrderException(ex.Message, ex);
        }
    }

    //bounos method

    /// <summary>
    /// bonus Update Order item amount  add
    /// for manager
    /// </summary>
    /// <param name="id"></param>
    /// <returns>BO.Order</returns>
    public void UpdateOIADD(int orderID, int productID)
    {
        if(Dal.OrderItem.GetAll().Any(i=>i.OrderID==orderID&&i.ProdectID==productID))
            throw new BO.WorngOrderException("the order item already exist in item");
        BO.OrderItem newOi=new BO.OrderItem();
        newOi= buildOIFromP(orderID, productID);
        DO.OrderItem item = new DO.OrderItem();
        item.ProdectID = newOi.ProdectID;
        item.Amount = newOi.Amount;
        item.Price = newOi.Price;
        item.OrderID=orderID;
        try
        {
            Dal.OrderItem.Add(item);
        }
        catch (Exception ex)
        {

            throw new BO.WorngOrderException(ex.Message,ex);
        }
    }
    /// <summary>
    /// bonus Update Order item delete
    /// for manager
    /// </summary>
    /// <param name="orderID"></param>
    /// <param name="proudctID"></param>
    public void updateOIdelete(int orderID, int proudctID)
    {
        IEnumerable<DO.OrderItem> list = Dal.OrderItem.GetAll();
        try
        {
            foreach (var item in list.ToList())
            {
                if (item.OrderID == orderID && item.ProdectID == proudctID)
                {
                     Dal.OrderItem.Delete(item.OrderItemID);
                     return;
                }
            }
        }
        catch (Exception ex)
        {

            throw new BO.WorngOrderException(ex.Message, ex);
        }
    }

    /// <summary>
    /// bonus Update Order item change amount
    /// for manager
    /// </summary>
    /// <param name="orderID"></param>
    /// <param name="proudctID"></param>

    public void updateOIAmount(int orderID, int proudctID, int amount)
    {
            foreach (var item in Dal.OrderItem.GetAll())
            {
                if (item.ProdectID == proudctID&&item.OrderID==orderID)
                {
                 DO.OrderItem NOI = new DO.OrderItem();
                 NOI.OrderID = orderID;
                 NOI.Amount = amount;
                NOI.Price = item.Price;
                NOI.ProdectID = proudctID;
                NOI.OrderItemID=item.OrderItemID;
                Dal.OrderItem.Update(NOI);
                return;
                }


            }
    }


    ///Help method


    /// <summary>
    /// calculate the amount of items of one order according id and the total price
    /// </summary>
    /// <param name="id"></param>
    /// <param name="orderItems"></param>
    /// <returns>Tuple<int ,Double></returns>
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
    /// <summary>
    /// convert BO.order to DO.order
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public BO.Order BuildOrderBO(DO.Order order)
    {
        BO.Order BOorder =new BO.Order();

        BOorder.ID = order.ID;
        BOorder.CustomerName = order.CustomerName;
        BOorder.CustomerAdress = order.CustomerAdress;
        BOorder.CustomerEmail = order.CustomerEmail;
        BOorder.OrderDate = order.OrderDate;
        BOorder.ShipDate = order.ShipDate;
        BOorder.DeliveryDate = order.DeliveryDate;
        BOorder.PaymentDate = BOorder.OrderDate;

        ///chack the status

        if (order.DeliveryDate != null)
            BOorder.Status = BO.OrderStatus.Deliverd;
        else if (order.ShipDate != null)
            BOorder.Status = BO.OrderStatus.Shiped;
        else
            BOorder.Status = BO.OrderStatus.Confirmed;

        ///list of orderitem
        IEnumerable<DO.OrderItem> ListOrderItem = Dal.OrderItem.GetAll();


        ///HelpOrder , it need for method "CalcAmountAndTotal"
        BlImplementation.Order HelpOrder = new BlImplementation.Order();

        var tuple = HelpOrder.CalcAmountAndTotal(BOorder.ID, ListOrderItem);

        BOorder.TotalPrice = tuple.Item2;

        ///build the list with "linq" and constractor of BO.OrderItem
        BOorder.Items =
                (from OrderItem in ListOrderItem
                 where OrderItem.OrderID == BOorder.ID
                 select BuildOrderItemBO(OrderItem)).ToList();

        return BOorder;


    }



    /// <summary>
    /// constractor that take DO.OrderItem
    /// </summary>
    /// <param name="orderItem"></param>
    public BO.OrderItem BuildOrderItemBO(DO.OrderItem orderItem)
    {
        BO.OrderItem BOorderItem = new BO.OrderItem();

        BOorderItem.ID = orderItem.OrderItemID;
        BOorderItem.ProdectID = orderItem.ProdectID;
        BOorderItem.Price = orderItem.Price;
        BOorderItem.Amount = orderItem.Amount;
        BOorderItem.TotalPrice = orderItem.Price * orderItem.Amount;


        ///find the name of the product

        IEnumerable<DO.Product> ListOrderItem = Dal.Product.GetAll();



        foreach (var item in ListOrderItem)
        {
            if (BOorderItem.ID == item.ID)
            {
                BOorderItem.Name = item.Name;
                break;
            }
        }

        return BOorderItem;
    }
    /// <summary>
    /// help function to chack status
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    internal BO.OrderStatus CheckStatus(DO.Order item)
    {
        BO.OrderStatus Status =new BO.OrderStatus();

        if (item.DeliveryDate != null)
            Status = BO.OrderStatus.Deliverd;
        else if (item.ShipDate != null)
            Status = BO.OrderStatus.Shiped;
        else
            Status = BO.OrderStatus.Confirmed;

        return Status;
    }

    public BO.OrderItem buildOIFromP(int OId ,int PId)
    {
        //check if the product exist
        if (Dal.Product.GetAll().Any(i => i.ID == PId))
        {
            BO.Product product = new BO.Product();
            foreach (var item in Dal.Product.GetAll())
            {
                if (item.ID == PId)
                {
                    product.ID = item.ID;
                    product.Name = item.Name;
                    product.Price = item.Price;
                    product.InStock = item.InStock;
                    product.Category = (BO.Category)item.Category;
                    break;
                }
            }
            BO.OrderItem newOI = new BO.OrderItem();
            newOI.Price = product.Price;
            newOI.Name = product.Name;
            newOI.ProdectID = product.ID;
            newOI.Amount = 1;
            newOI.TotalPrice = newOI.Price;
            return newOI;  
        }
        else
            throw new BO.WorngOrderException("the product is not exist");
    }
}


