namespace Dal;
using  DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Xml.Linq;

internal class Product : IProduct
{
    const string s_Product = "Products";
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
        XElement ProductsRootElem = XMLTools.LoadListFromXMLElement(s_Product);

        XElement? pr = (from st in ProductsRootElem.Elements()
                          where st.ToIntNullable("ID") == prod.ID //where (int?)st.Element("ID") == doStudent.ID
                          select st).FirstOrDefault();
        if (pr != null)
            throw new AllreadyExistException("the item is allready exist"); 

        XElement prodElem = new XElement("Product",
                                   new XElement("ID", prod.ID),
                                   new XElement("Name", prod.Name),
                                   new XElement("Price", prod.Price),
                                   new XElement("Category", prod.Category),                         
                                   new XElement("InStock", prod.InStock)
                                   );

        ProductsRootElem.Add(prodElem);

        XMLTools.SaveListToXMLElement(ProductsRootElem, s_Product);

        return prod.ID; 
    }

    public void Delete(int id)
    {
        XElement ProductsRootElem = XMLTools.LoadListFromXMLElement(s_Product);

        XElement? pr = (from st in ProductsRootElem.Elements()
                          where (int?)st.Element("ID") == id
                          select st).FirstOrDefault() ?? throw new NotFoundException("the product not found");  

        pr.Remove(); //<==>   Remove Product from Productlist

        XMLTools.SaveListToXMLElement(ProductsRootElem, s_Product);
    }

    public DO.Product? Get(int id)
    {
        XElement ProductsRootElem = XMLTools.LoadListFromXMLElement(s_Product); 

        return (from s in ProductsRootElem?.Elements()
                where s.ToIntNullable("ID") == id
                select (DO.Product?)createProductfromXElement(s)).FirstOrDefault()
                ?? throw new NotFoundException("the product not found"); 
    }

    public DO.Product? Get(Func<DO.Product?, bool>? predict)
    {
        XElement ProductsRootElem = XMLTools.LoadListFromXMLElement(s_Product);

        return (from s in ProductsRootElem?.Elements()
                where predict(createProductfromXElement(s))
                select (DO.Product?)createProductfromXElement(s)).FirstOrDefault()
                ?? throw new NotFoundException("the product not found");
    }

    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? filter = null)
    {
        XElement ProductsRootElem = XMLTools.LoadListFromXMLElement(s_Product);

        if (filter != null)
        {
            return from s in ProductsRootElem.Elements()
                   let pr = createProductfromXElement(s)
                   where filter(pr)
                   select (DO.Product?)pr;
        }
        else
        {
            return from s in ProductsRootElem.Elements()
                   select createProductfromXElement(s);
        }

    }

    public void Update(DO.Product prod)
    {
        Delete(prod.ID);
        Add(prod);
    }
}