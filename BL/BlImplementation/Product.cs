using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using Dal;
using DalApi;


namespace BlImplementation;

internal class Product : BlApi.IProduct
{
    private IDal Dal = new DalList();
    /// <summary>
    /// add a product for data base   for manager
    /// </summary>
    /// <param name="product"></param>
    public void Add(BO.Product product)
    {
        if (product.ID < 100000 || product.ID > 999999)
            throw new BO.WorngProductException("id not valid");
        if (product.Name == null)
            throw new BO.WorngProductException("name not valid");
        if (product.Price < 0)
            throw new BO.WorngProductException("price not valid");
        if (product.InStock < 0)
            throw new BO.WorngProductException("InStock not valid");
        DO.Product p_DO = new DO.Product();
        p_DO.ID = product.ID;
        p_DO.Name = product.Name;
        p_DO.Price = product.Price;
        p_DO.InStock = product.InStock;
        p_DO.Category = (DO.Category)product.Category;
        try
        {
            Dal.Product.Add(p_DO);
        }
        catch (AllreadyExistException ex)
        {
            //Console.WriteLine(ex.Message); 
            throw ex;
        }
    
    }
    /// <summary>
    /// delete product from data base   for manager
    /// </summary>
    /// <param name="id"></param>
    public void Delete(int id)
    {
        if (id < 100000 || id > 999999)
            throw new BO.WorngProductException("id not valid");
        foreach(var item in Dal.OrderItem.GetAll())
        {
            if (item.ProdectID == id)
                throw new BO.WorngProductException("the product to delete is in a order");
        }
        try 
        {
            Dal.Product.Delete(id);
        }
        catch(NotFoundException ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// get product details for client
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cart"></param>
    /// <returns>BO.ProductItem</returns>
    public BO.ProductItem GetForClient(int id, BO.Cart cart)
    {
        if (id < 100000 || id > 999999)
            throw new BO.WorngProductException("id not valid");
        try
        {
            DO.Product DP = Dal.Product.Get(id);
            BO.ProductItem PItem = new BO.ProductItem();
            PItem.ID = DP.ID;
            PItem.Name = DP.Name;
            PItem.Price = DP.Price;
            PItem.Category = (BO.Category)DP.Category;//convertion
            if (DP.InStock > 0)
                PItem.InStock = true;
            else
                PItem.InStock = false;
            PItem.Amount = 0;
            foreach (var item in cart.Items)
            {
                if (item.ProdectID==id)
                {
                    PItem.Amount = item.Amount;
                    break;
                }
            }
            return PItem;
        }
        catch(NotFoundException ex)
        {
            throw ex;
        }

    }
    /// <summary>
    /// get product details for manager
    /// </summary>
    /// <param name="id"></param>
    /// <returns>BO.Product</returns>
    public BO.Product GetForManager(int id)
    {
        if (id < 100000 || id > 999999)
            throw new BO.WorngProductException("id not valid");
        try
        {
            DO.Product DP = Dal.Product.Get(id);
            BO.Product BP=new BO.Product();
            BP.ID = DP.ID;
            BP.Name = DP.Name;
            BP.Price = DP.Price;
            BP.InStock= DP.InStock;
            BP.Category = (BO.Category)DP.Category;
            return BP;
        }
        catch (NotFoundException ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// get list of productForList
    /// </summary>
    /// <returns>IEnumerable<BO.Product></returns>
    public IEnumerable<BO.ProductForList> GetList()
    {
        IEnumerable<DO.Product> list = Dal.Product.GetAll();
        List<BO.ProductForList> list2 = new List<BO.ProductForList>();
        foreach (DO.Product item in list)
        {
            BO.ProductForList PFLItem = new BO.ProductForList(item);
            list2.Add(PFLItem);
        }
        return list2;
       
    }
    /// <summary>
    /// update a product   for manager
    /// </summary>
    /// <param name="product"></param>
    public void Update(BO.Product product)
    {
        if (product.ID < 100000 || product.ID > 999999)
            throw new BO.WorngProductException("id not valid");
        if (product.Name == null)
            throw new BO.WorngProductException("name not valid");
        if (product.Price < 0)
            throw new BO.WorngProductException("price not valid");
        if (product.InStock < 0)
            throw new BO.WorngProductException("InStock not valid");
        DO.Product p_DO = new DO.Product();
        p_DO.ID = product.ID;
        p_DO.Name = product.Name;
        p_DO.Price = product.Price;
        p_DO.InStock = product.InStock;
        p_DO.Category = (DO.Category)product.Category;
        try
        {
            Dal.Product.Update(p_DO);
        }
        catch (AllreadyExistException ex)
        {
            //Console.WriteLine(ex.Message); 
            throw ex;
        }
    }

    
}
