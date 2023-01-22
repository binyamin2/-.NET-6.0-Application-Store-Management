using DalApi;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Dal;
internal class DalOrder : IOrder
{
    public string s_Order = "Orders";
    
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
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(DO.Order order)
    {
        XElement OrderRootElem = XMLTools.LoadListFromXMLElement(s_Order);

        ///take "config.xml" for the index

        XElement ConfigRootElem = XMLTools.LoadListFromXMLElement("Config");

        int id = (int)ConfigRootElem.Element("OrderIndex");

        order.ID = id;


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


        ConfigRootElem.Element("OrderIndex")?.SetValue(id);


        XMLTools.SaveListToXMLElement(ConfigRootElem, "Config");




        OrderRootElem.Add(OrderElem);

        XMLTools.SaveListToXMLElement(OrderRootElem, s_Order);

        return order.ID; 
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        XElement OrderRootElem = XMLTools.LoadListFromXMLElement(s_Order);

        XElement? order = (from or in OrderRootElem.Elements()
                           where (int?)or.Element("ID") == id
                           select or).FirstOrDefault() ?? throw new NotFoundException("the Order not found");

        order.Remove(); //<==>   Remove order from OrderRootElem

        XMLTools.SaveListToXMLElement(OrderRootElem, s_Order);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Order? Get(int id)
    {
        XElement OrderRootElem = XMLTools.LoadListFromXMLElement(s_Order);

        return (from s in OrderRootElem?.Elements()
                where s.ToIntNullable("ID") == id
                select (DO.Order?)createOrderfromXElement(s)).FirstOrDefault()
                ?? throw new NotFoundException("the Order not found");
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Order? Get(Func<DO.Order?, bool>? predict)
    {
        XElement OrderRootElem = XMLTools.LoadListFromXMLElement(s_Order);

        return (from s in OrderRootElem?.Elements()
                where predict(createOrderfromXElement(s))
                select (DO.Order?)createOrderfromXElement(s)).FirstOrDefault()
                ?? throw new NotFoundException("the Order not found\"");
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? predict = null)
    {
        XElement OrderRootElem = XMLTools.LoadListFromXMLElement(s_Order);

        

        if (predict != null)
        {
            return from s in OrderRootElem.Elements()
                   let doOrd = createOrderfromXElement(s)
                   where predict(doOrd)
                   select (DO.Order?)doOrd;
        }
        else
        {

            return from s in OrderRootElem.Elements()
                   select createOrderfromXElement(s);
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(DO.Order order)
    {
        XElement OrderRootElem = XMLTools.LoadListFromXMLElement(s_Order);

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
        new_order.Remove();

        OrderRootElem.Add(OrderElem);
        
        XMLTools.SaveListToXMLElement(OrderRootElem, s_Order);
    }

    
}
