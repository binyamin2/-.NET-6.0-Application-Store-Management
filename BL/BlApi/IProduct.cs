using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;

public interface IProduct
{
    /// <summary>
    /// get list of product
    /// </summary>
    /// <returns>IEnumerable<BO.Product></returns>
    public IEnumerable<BO.ProductForList> GetList();
    /// <summary>
    /// get product details for manager
    /// </summary>
    /// <param name="id"></param>
    /// <returns>BO.Product</returns>
    public BO.Product GetForManager(int id);
    /// <summary>
    /// get product details for client
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cart"></param>
    /// <returns>BO.ProductItem</returns>
    public BO.ProductItem GetForClient(int id,BO.Cart cart);
    /// <summary>
    /// add a product for data base   for manager
    /// </summary>
    /// <param name="product"></param>
    public void Add(BO.Product product);
    /// <summary>
    /// delete product from data base   for manager
    /// </summary>
    /// <param name="id"></param>
    public void Delete(int id);
    /// <summary>
    /// update a product   for manager
    /// </summary>
    /// <param name="product"></param>
    public void Update(BO.Product product);


}
