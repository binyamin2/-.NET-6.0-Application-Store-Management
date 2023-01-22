using DalApi;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Dal;
internal class DalOrderItem : IOrderItem
{
    const string s_Oi = "OrderItems";
    /// <summary>
    /// create order item from xwlement
    /// </summary>
    /// <param name="s"></param>
    /// <returns>OrderItem</returns>
    /// <exception cref="FormatException"></exception>
    static DO.OrderItem? createOIfromXElement(XElement s)
    {
        return new DO.OrderItem()
        {
            ID = s.ToIntNullable("ID") ?? throw new FormatException("id"),
            ProductID = s.ToIntNullable("ProductID") ?? throw new FormatException("product id"),
            OrderID = s.ToIntNullable("OrderID") ?? throw new FormatException("order id"),
            Price = (double)s.Element("Price")!,
            Amount = s.ToIntNullable("Amount") ?? throw new FormatException("Amount")
        };
    }
    /// <summary>
    /// add Order Item to the file
    /// </summary>
    /// <param name="OI"></param>
    /// <returns>int </returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(DO.OrderItem OI)
    {
        XElement OIRootElem = XMLTools.LoadListFromXMLElement(s_Oi);

        XElement ConfigRootElem = XMLTools.LoadListFromXMLElement("Config");
        //find the current order item id and put it in OI
        int id = (int)ConfigRootElem.Element("OrderItemIndex")!;
        
        OI.ID = id;
        //build xelement oi
        XElement OIElem = new XElement("OrderItem",
                                   new XElement("ID", OI.ID),
                                   new XElement("ProductID", OI.ProductID),
                                   new XElement("OrderID", OI.OrderID),
                                   new XElement("Price", OI.Price),
                                   new XElement("Amount", OI.Amount)
                                   );
        //update the files
        id++;

        ConfigRootElem.Element("OrderItemIndex")?.SetValue(id);

        XMLTools.SaveListToXMLElement(ConfigRootElem, "Config");

        OIRootElem.Add(OIElem);

        XMLTools.SaveListToXMLElement(OIRootElem, s_Oi);

        return OI.ID;
    }
    /// <summary>
    /// delete order item from file
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="NotFoundException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        XElement OIRootElem = XMLTools.LoadListFromXMLElement(s_Oi);
        //find th oi to delete with linq
        XElement? oi = (from st in OIRootElem.Elements()
                        where (int?)st.Element("ID") == id
                        select st).FirstOrDefault() ?? throw new NotFoundException("the orderitem not found");

        oi.Remove(); //   Remove Product from Productlist

        XMLTools.SaveListToXMLElement(OIRootElem, s_Oi);
    }
    /// <summary>
    /// get order item by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>OrderItem</returns>
    /// <exception cref="NotFoundException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.OrderItem? Get(int id)
    {
        XElement OIRootElem = XMLTools.LoadListFromXMLElement(s_Oi);
        //get the order item by linq
        return (from s in OIRootElem?.Elements()
                where s.ToIntNullable("ID") == id
                select (DO.OrderItem?)createOIfromXElement(s)).FirstOrDefault()
                ?? throw new NotFoundException("the orderitem not found");
    }
    /// <summary>
    /// get order item by predict
    /// </summary>
    /// <param name="predict"></param>
    /// <returns>OrderItem</returns>
    /// <exception cref="NotFoundException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.OrderItem? Get(Func<DO.OrderItem?, bool>? predict)
    {
        XElement OIRootElem = XMLTools.LoadListFromXMLElement(s_Oi);
        //get the order item by linq
        return (from s in OIRootElem?.Elements()
                where predict(createOIfromXElement(s))
                select (DO.OrderItem?)createOIfromXElement(s)).FirstOrDefault()
                ?? throw new NotFoundException("the orderitem not found");
    }
    /// <summary>
    /// get the all file to list by filter
    /// </summary>
    /// <param name="filter"></param>
    /// <returns>IEnumerable<DO.OrderItem?></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? filter = null)
    {
        XElement OIRootElem = XMLTools.LoadListFromXMLElement(s_Oi);
        //return the wanted elements
        if (filter != null)
        {
            return from s in OIRootElem.Elements()
                   let oi = createOIfromXElement(s)
                   where filter(oi)
                   select (DO.OrderItem?)oi;
        }
        //return all file
        else
        {
            return from s in OIRootElem.Elements()
                   select createOIfromXElement(s);
        }

    }
    /// <summary>
    /// return order item by order and product id
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="productId"></param>
    /// <returns>orderitem</returns>
    /// <exception cref="NotFoundException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.OrderItem? GetItemByOrderAndProduct(int orderId, int productId)
    {
        XElement OIRootElem = XMLTools.LoadListFromXMLElement(s_Oi);
        //return the wanted order item with linq
        return (from s in OIRootElem?.Elements()
                where s.ToIntNullable("ProductID") == productId && s.ToIntNullable("OrderID") == orderId
                select (DO.OrderItem?)createOIfromXElement(s)).FirstOrDefault()
                ?? throw new NotFoundException("the orderitem not found");
    }

    /// <summary>
    /// return order item by order id
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns>IEnumerable<DO.OrderItem?></returns>
    /// <exception cref="NotFoundException"></exception>
    public IEnumerable<DO.OrderItem?> GetItemsListByOrderId(int orderId)
    {
        XElement OIRootElem = XMLTools.LoadListFromXMLElement(s_Oi);
        //return the wanted order item with linq
        return (from s in OIRootElem?.Elements()
                where s.ToIntNullable("OrderID") == orderId
                select (DO.OrderItem?)createOIfromXElement(s))
                ?? throw new NotFoundException("the orderitem not found");
    }
    /// <summary>
    /// update order item
    /// </summary>
    /// <param name="oi"></param>
    /// <exception cref="NotFoundException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(DO.OrderItem oi)
    {
        XElement OIRootElem = XMLTools.LoadListFromXMLElement(s_Oi);
        //find the wanted order item
        XElement? noi = (from st in OIRootElem.Elements()
                         where (int?)st.Element("ID") == oi.ID
                         select st).FirstOrDefault() ?? throw new NotFoundException("the orderitem not found");
        //update the details and save the file
        noi.Element("ID")!.SetValue(oi.ID);
        noi.Element("ProductID")!.SetValue(oi.ProductID);
        noi.Element("OrderID")!.SetValue(oi.OrderID);
        noi.Element("Price")!.SetValue(oi.Price);
        noi.Element("Amount")!.SetValue(oi.Amount);

        XMLTools.SaveListToXMLElement(OIRootElem, s_Oi);
    }
}