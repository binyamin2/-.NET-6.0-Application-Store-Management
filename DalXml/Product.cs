namespace Dal;
using  DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Xml.Linq;

internal class Product : IProduct
{
    static DO.Product? createProductfromXElement(XElement s)
    {
        return new DO.Product()
        {
            ID = s.ToIntNullable("ID") ?? throw new FormatException("id"), //fix to: DalXmlFormatException(id)),
            Name = (string?)s.Element("Name"),
            Price = (double)s.Element("Price"),
            Category = s.ToEnumNullable<DO.Category>("StudentStatus"),
            InStock = s.ToIntNullable("Instock") ?? throw new FormatException("Instock")
        };
    }
    public int Add(DO.Product prod)
    {
        XElement ProductsRootElem = XMLTools.LoadListFromXMLElement("Products");

        XElement? pr = (from st in ProductsRootElem.Elements()
                          where st.ToIntNullable("ID") == prod.ID //where (int?)st.Element("ID") == doStudent.ID
                          select st).FirstOrDefault();
        if (pr != null)
            throw new AllreadyExistException("the item is allready exist"); 

        XElement studentElem = new XElement("Product",
                                   new XElement("ID", prod.ID),
                                   new XElement("Name", prod.Name),
                                   new XElement("Price", prod.Price),
                                   new XElement("Category", prod.Category),                         
                                   new XElement("InStock", prod.InStock)
                                   );

        ProductsRootElem.Add(studentElem);

        XMLTools.SaveListToXMLElement(ProductsRootElem, "Products");

        return prod.ID; 
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