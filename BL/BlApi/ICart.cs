using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;
using BO;

/// <summary>
/// interface for cart
/// </summary>
public interface ICart
{
    /// <summary>
    /// Add product to cart
    /// for catalog and Detail order
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <returns>BO.Cart</returns>
    public BO.Cart Add(Cart cart, int? id);
    /// <summary>
    /// Update amount of order
    /// for cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public BO.Cart Update(Cart cart, int? id, int? amount);
    /// <summary>
    /// Confirm Order 
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="DetailClient"></param>
    public void ConfirmOrder(Cart cart,Tuple<string? ,string?,string?> DetailClient);
}
