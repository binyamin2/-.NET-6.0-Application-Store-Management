namespace Dal;
using  DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Xml.Linq;

internal class Product : IProduct
{
    static DO.Product? createStudentfromXElement(XElement s)
    {
        return new DO.Product()
        {
            ID = s.ToIntNullable("ID") ?? throw new FormatException("id"), //fix to: DalXmlFormatException(id)),
            Name = (string?)s.Element("Name"),
            Price = (double)s.Element("Price"),
            StudentStatus = s.ToEnumNullable<DO.StudentStatus>("StudentStatus"),
            BirthDate = s.ToDateTimeNullable("BirthDate"),
            Grade = (double?)s.Element("Grade") // s.ToDoubleNullable("Grade")
        };
    }
    public int Add(DO.Product value)
    {
        throw new NotImplementedException();
    }

    public void Delete(int value)
    {
        throw new NotImplementedException();
    }

    public DO.Product? Get(int id)
    {
        throw new NotImplementedException();
    }

    public DO.Product? Get(Func<DO.Product?, bool>? predict)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? predict = null)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Product value)
    {
        throw new NotImplementedException();
    }
}