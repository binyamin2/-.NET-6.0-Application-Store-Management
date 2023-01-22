using DalApi;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Dal;
internal class DalOrder : IOrder
{
    public string s_Order = "Orders";
    /// <summary>
    /// create order from xelement
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    /// <exception cref="FormatException"></exception>
    static DO.Order? createOrderfromXElement(XElement s)
    {
        return new DO.Order()
        {
            ID = s.ToIntNullable("ID") ?? throw new FormatException("id"),
            CustomerName = (string?)s.Element("CustomerName"),
            CustomerEmail = (string?)s.Element("CustomerEmail"),
            CustomerAdress = (string?)s.Element("CustomerAdress"),
            OrderDate = s.ToDateTimeNullable("OrderDate"),
            ShipDate = s.ToDateTimeNullable("ShipDate"),
            DeliveryDate = s.ToDateTimeNullable("DeliveryDate"),
        };
    }
    /// <summary>
    /// adding order to orders
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(DO.Order order)
    {
        //loading the root
        XElement OrderRootElem = XMLTools.LoadListFromXMLElement(s_Order);

        ///take "config.xml" for the index

        XElement ConfigRootElem = XMLTools.LoadListFromXMLElement("Config");

        int id = (int)ConfigRootElem.Element("OrderIndex")!;

        order.ID = id;

        //build new xelement
        XElement OrderElem = new XElement("Order",
                                   new XElement("ID", order.ID),
                                   new XElement("CustomerName", order.CustomerName),
                                   new XElement("CustomerEmail", order.CustomerEmail),
                                   new XElement("CustomerAdress", order.CustomerAdress),
                                   new XElement("OrderDate", order.OrderDate),
                                   new XElement("ShipDate", order.ShipDate),
                                   new XElement("DeliveryDate", order.DeliveryDate)
                                   );

        id++;

        //update the id 
        ConfigRootElem.Element("OrderIndex")?.SetValue(id);

        //save the id in the config file
        XMLTools.SaveListToXMLElement(ConfigRootElem, "Config");



        //add and save the list
        OrderRootElem.Add(OrderElem);

        XMLTools.SaveListToXMLElement(OrderRootElem, s_Order);

        return order.ID; 
    }
    /// <summary>
    /// delete order from the file
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="NotFoundException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        XElement OrderRootElem = XMLTools.LoadListFromXMLElement(s_Order);
        //find the order to delete
        XElement? order = (from or in OrderRootElem.Elements()
                           where (int?)or.Element("ID") == id
                           select or).FirstOrDefault() ?? throw new NotFoundException("the Order not found");

        order.Remove(); //  Remove order from OrderRootElem
        //save
        XMLTools.SaveListToXMLElement(OrderRootElem, s_Order);
    }
    /// <summary>
    /// get the order by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Order? Get(int id)
    {
        XElement OrderRootElem = XMLTools.LoadListFromXMLElement(s_Order);
        //using linq to find and return the order
        return (from s in OrderRootElem?.Elements()
                where s.ToIntNullable("ID") == id
                select (DO.Order?)createOrderfromXElement(s)).FirstOrDefault()
                ?? throw new NotFoundException("the Order not found");
    }
    /// <summary>
    /// get order by predict
    /// </summary>
    /// <param name="predict"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Order? Get(Func<DO.Order?, bool>? predict)
    {
        XElement OrderRootElem = XMLTools.LoadListFromXMLElement(s_Order);
        //using linq to find and return the order
        return (from s in OrderRootElem?.Elements()
                where predict(createOrderfromXElement(s))
                select (DO.Order?)createOrderfromXElement(s)).FirstOrDefault()
                ?? throw new NotFoundException("the Order not found\"");
    }
    /// <summary>
    /// get the all file of order by predict
    /// </summary>
    /// <param name="predict"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? predict = null)
    {
        XElement OrderRootElem = XMLTools.LoadListFromXMLElement(s_Order);
        //return all the file
        if (predict != null)
        {
            return from s in OrderRootElem.Elements()
                   let doOrd = createOrderfromXElement(s)
                   where predict(doOrd)
                   select (DO.Order?)doOrd;
        }
        //return the wanted element
        else
        {

            return from s in OrderRootElem.Elements()
                   select createOrderfromXElement(s);
        }
    }
    /// <summary>
    /// update order
    /// </summary>
    /// <param name="order"></param>
    /// <exception cref="NotFoundException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(DO.Order order)
    {
        XElement OrderRootElem = XMLTools.LoadListFromXMLElement(s_Order);
        //find the order to update
        XElement? new_order = (from or in OrderRootElem.Elements()
                           where (int?)or.Element("ID") == order.ID
                           select or).FirstOrDefault() ?? throw new NotFoundException("the Order not found");
        XElement OrderElem = new XElement("Order",
                                   new XElement("ID", order.ID),
                                   new XElement("CustomerName", order.CustomerName),
                                   new XElement("CustomerEmail", order.CustomerEmail),
                                   new XElement("CustomerAdress", order.CustomerAdress),
                                   new XElement("OrderDate", order.OrderDate),
                                   new XElement("ShipDate", order.ShipDate),
                                   new XElement("DeliveryDate", order.DeliveryDate));
        //remove the old order and add the updated order
        new_order.Remove();

        OrderRootElem.Add(OrderElem);
        
        XMLTools.SaveListToXMLElement(OrderRootElem, s_Order);
    }

    
}
