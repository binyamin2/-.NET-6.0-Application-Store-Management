using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using BlApi;


using DalApi;


namespace BlImplementation;

internal class Product : BlApi.IProduct
{
    private IDal? Dal = DalApi.Factory.Get();


    /// <summary>
    /// add a product for data base   for manager
    /// </summary>
    /// <param name="product"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Add(BO.Product product)
    {
        lock (Dal!)
        {
            if (product.ID < 100000 || product.ID > 999999)
                throw new BO.WorngProductException("id not valid");
            if (product.Name == "" || product.Name == null)
                throw new BO.WorngProductException("name not valid");
            if (product.Price < 0)
                throw new BO.WorngProductException("price not valid");
            if (product.InStock < 0)
                throw new BO.WorngProductException("InStock not valid");
            DO.Product p_DO = new DO.Product();
            CopyProperties<DO.Product, BO.Product>.Copy(ref p_DO, product);

            p_DO.Category = (DO.Category)product.Category!;
            try
            {
                Dal!.Product.Add(p_DO);
            }
            catch (Exception ex)
            {
                throw new BO.WorngProductException(ex.Message, ex);
            }
        }
    }
    /// <summary>
    /// delete product from data base   for manager
    /// </summary>
    /// <param name="id"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {   lock (Dal!)
        {
            if (id < 100000 || id > 999999)
                throw new BO.WorngProductException("id not valid");
            foreach (var item in Dal!.OrderItem.GetAll())
            {
                if (item?.ProductID == id)//update the amount in stock to zero and throw exeption that the product in order
                {
                    DO.Product p = (DO.Product)Dal.Product.Get(id)!;
                    p.InStock = 0;
                    Dal.Product.Update(p);
                    throw new BO.WorngProductException("the product to delete is in a order, from now and on there is no option to order the product");
                }
            }
            try
            {
                Dal.Product.Delete(id);
            }
            catch (Exception ex)
            {
                throw new BO.WorngProductException(ex.Message, ex);
            }
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductForList> GetCategory(BO.CategoryUI category)
    {
        lock (Dal!)
        {
            if (category == BO.CategoryUI.All)
            {
                return GetList();
            }

            var result = from ProductForList in GetList()
                         group ProductForList by ProductForList.Category into categorys
                         select categorys;


            return result.First(i => (int)i.Key! == (int)category);

        }
    }


    /// <summary>
    /// get product details for client
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cart"></param>
    /// <returns>BO.ProductItem</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.ProductItem GetForClient(int id, BO.Cart cart)
    {
        lock (Dal!)
        {
            if (id < 100000 || id > 999999)
                throw new BO.WorngProductException("id not valid");
            try
            {
                DO.Product? DP = Dal!.Product.Get(id);
                BO.ProductItem PItem = new BO.ProductItem();
                CopyProperties<BO.ProductItem, DO.Product?>.Copy(ref PItem!, DP);
                PItem!.Category = (BO.Category)DP?.Category!;//convertion
                if (DP?.InStock > 0)
                    PItem.InStock = true;
                else
                    PItem.InStock = false;
                PItem.Amount = 0;
                if (cart.Items!.Count != 0)
                {
                    PItem.Amount = cart.Items.First(i => i.ProductID == id)!.Amount;



                }
                return PItem;
            }
            catch (Exception ex)
            {

                throw new BO.WorngProductException(ex.Message, ex);
            }
        }
    }
    /// <summary>
    /// get product details for manager
    /// </summary>
    /// <param name="id"></param>
    /// <returns>BO.Product</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Product GetForManager(int id)
    {
        lock (Dal!)
        {
            if (id < 100000 || id > 999999)
                throw new BO.WorngProductException("id not valid");
            //try to get the right product 
            try
            {
                DO.Product? DP = Dal!.Product.Get(id);
                BO.Product BP = new BO.Product();
                CopyProperties<BO.Product, DO.Product?>.Copy(ref BP, DP);
                BP!.Category = (BO.Category)DP?.Category!;
                return BP;
            }
            catch (Exception ex)
            {

                throw new BO.WorngProductException(ex.Message, ex);
            }
        }
    }
    /// <summary>
    /// get list of productForList
    /// </summary>
    /// <returns>IEnumerable<BO.Product></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductForList> GetList()
    {
        lock (Dal!)
        {
            IEnumerable<DO.Product?> list = Dal!.Product.GetAll();
            List<BO.ProductForList> list2 = new List<BO.ProductForList>();
            foreach (DO.Product item in list)//converting from product to product for list
            {
                BO.ProductForList PFLItem = new BO.ProductForList();
                CopyProperties<BO.ProductForList, DO.Product>.Copy(ref PFLItem, item);
                PFLItem!.Category = (BO.Category)item.Category!;
                list2.Add(PFLItem);
            }
            return list2.OrderBy(x => x.ID);
        }
    }


    /// <summary>
    /// update a product   for manager
    /// </summary>
    /// <param name="product"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(BO.Product product)
    {
        lock (Dal!)
        {//check the input
            if (product.ID < 100000 || product.ID > 999999)
                throw new BO.WorngProductException("id not valid");
            if (product.Name == null)
                throw new BO.WorngProductException("name not valid");
            if (product.Price < 0)
                throw new BO.WorngProductException("price not valid");
            if (product.InStock < 0)
                throw new BO.WorngProductException("InStock not valid");
            //build new product for update
            DO.Product p_DO = new DO.Product();
            CopyProperties<DO.Product, BO.Product>.Copy(ref p_DO, product);
            p_DO.Category = (DO.Category)product.Category!;
            try//asking from dal to update deteils
            {
                Dal!.Product.Update(p_DO);
            }
            catch (Exception ex)
            {

                throw new BO.WorngProductException(ex.Message, ex);
            }
        }
    }
    /// <summary>
    /// return list of ProductItem For window ListProductItem
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductItem> GetListProductItems(BO.CategoryUI category = BO.CategoryUI.All)
    {
        lock (Dal!)
        {
            IEnumerable<DO.Product?> list = new List<DO.Product?>();
            if (category == BO.CategoryUI.All)
            {
                list = Dal!.Product.GetAll();
            }
            else
            {
                list = from product in Dal!.Product.GetAll()
                       where (int)product?.Category! == (int)category
                       orderby product?.ID
                       select product;
            }


            List<BO.ProductItem> list2 = new List<BO.ProductItem>();

            foreach (var item in list)
            {
                BO.ProductItem PItem = new BO.ProductItem();
                CopyProperties<BO.ProductItem, DO.Product?>.Copy(ref PItem!, item);
                PItem.Category = (BO.Category)item?.Category!;
                PItem.Amount = 0;
                PItem.Price = item?.Price;
                if (item?.InStock > 0)
                {
                    PItem.InStock = true;
                }
                else
                {
                    PItem.InStock = false;
                }
                list2.Add(PItem);
            }
            return list2;
        }

    }
}



