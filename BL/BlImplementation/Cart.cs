using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class Cart : BlApi.ICart
{
    private IDal Dal = new DalList();

    public BO.Cart Add(BO.Cart cart, int? id)
    {
        //return true if the product exist in the cart
        bool exist=cart.Items.Any(i => i.ProdectID == id);
        if(!exist)
        {
            //check if the product exist
            if (Dal.Product.GetAll().Any(i => i.ID == id))
            {
                BO.OrderItem newOI  = new BO.OrderItem();
            }
        }
        throw new NotImplementedException();
    }

    public void ConfirmOrder(BO.Cart cart, Tuple<string?, string?, string?> DetailClient)
    {
        throw new NotImplementedException();
    }

    public BO.Cart Update(BO.Cart cart, int? id, int? amount)
    {
        throw new NotImplementedException();
    }
}
