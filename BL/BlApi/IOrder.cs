using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi;

/// <summary>
/// interface for order
/// </summary>
public interface IOrder
{
    /// <summary>
    /// get list Order
    /// for manager
    /// </summary>
    /// <returns>IEnumerable<></returns>
    public IEnumerable<BO.OrderForList> GetList ();
    /// <summary>
    /// get order
    /// for manager and client
    /// </summary>
    /// <param name="id"></param>
    /// <returns>BO.Order</returns>
    public BO.Order Get (int id);
    /// <summary>
    /// Update ship
    /// for manager order
    /// </summary>
    /// <param name="id"></param>
    /// <returns>UpdateShip</returns>
    public BO.Order UpdateShip (int id);
    /// <summary>
    /// Update Delivery
    /// for manager order
    /// </summary>
    /// <param name="id"></param>
    /// <returns>BO.Order</returns>
    public BO.Order UpdateDelivery (int id);
    /// <summary>
    /// Order Tracking
    /// for manager order
    /// </summary>
    /// <param name="id"></param>
    /// <returns>OrderTracking </returns>
    public OrderTracking OrderTracking (int id);
    /// <summary>
    /// bonus Update Order item amount  add
    /// for manager
    /// </summary>
    /// <param name="id"></param>
    /// <returns>BO.Order</returns>
    public void UpdateOIADD (int orderID, int producID);
    /// <summary>
    /// bonus Update Order item delete
    /// for manager
    /// </summary>
    /// <param name="orderID"></param>
    /// <param name="proudctID"></param>
    public void updateOIdelete(int orderID, int proudctID);
    
    /// <summary>
    /// bonus Update Order item change amount
    /// for manager
    /// </summary>
    /// <param name="orderID"></param>
    /// <param name="proudctID"></param>

    public void updateOIAmount(int orderID, int proudctID, int amount);

    public IEnumerable<BO.Order> getAll(Func<BO.Order, bool>? func = null);

    public int nextOrder();
}
