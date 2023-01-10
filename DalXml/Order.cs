using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

internal class Order : IOrder
{
    static DO.Order? createOrderfromXElement(XElement s)
    {
        return new DO.Order()
        {
            ID = s.ToIntNullable("ID") ?? throw new FormatException("id"), //fix to: DalXmlFormatException(id)),
            CustomerName = (string?)s.Element("CustomerName"),
            CustomerEmail = (string?)s.Element("CustomerEmail"),
            CustomerAdress = (string?)s.Element("CustomerAdress"),
            OrderDate = s.ToDateTimeNullable("OrderDate"),
            ShipDate = s.ToDateTimeNullable("ShipDate"),
            DeliveryDate = s.ToDateTimeNullable("DeliveryDate"),
        };
    }
    public int Add(DO.Order value)
    {

        throw new NotImplementedException();
    }

    public void Delete(int value)
    {
        throw new NotImplementedException();
    }

    public DO.Order? Get(int id)
    {
        throw new NotImplementedException();
    }

    public DO.Order? Get(Func<DO.Order?, bool>? predict)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? predict = null)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Order value)
    {
        throw new NotImplementedException();
    }
}
