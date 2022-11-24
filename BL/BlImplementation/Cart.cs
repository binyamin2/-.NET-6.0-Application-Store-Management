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

    public BO.Cart Add(BO.Cart cart, int id)
    {
        //return true if the product exist in the cart
        bool exist=cart.Items.Any(i => i.ProdectID == id);
        if(!exist)
        {
            //check if the product exist
            if (Dal.Product.GetAll().Any(i => i.ID == id))
            {
                BO.Product product = new BO.Product();
                foreach (var item in Dal.Product.GetAll())
                {
                    if(item.ID == id)
                    {
                        product.ID = item.ID;
                        product.Name = item.Name;
                        product.Price= item.Price;
                        product.InStock = item.InStock;
                        product.Category= (BO.Category)item.Category;
                        break;
                    }
                }
                BO.OrderItem newOI  = new BO.OrderItem();
                newOI.Price = product.Price;
                newOI.Name=product.Name;
                newOI.ProdectID = product.ID;
                newOI.Amount = 1;
                newOI.TotalPrice=newOI.Price;
                cart.Items.Add(newOI);
                cart.TotalPrice += newOI.TotalPrice;
                return cart;

            }
        }
       
        bool instock = false;
        foreach (var item in Dal.Product.GetAll())
        {
            if (item.ID == id)
            {
                if (item.InStock > 0)
                {
                    instock = true;
                    break;
                }
            }
        }
        if (!instock)
        {
            throw new BO.NotInStockException("the product is over from stock");
        }
        UpDateOI(cart, id);
        return cart;
      
    }

    public void ConfirmOrder(BO.Cart cart, Tuple<string?, string?, string?> DetailClient)
    {
        throw new NotImplementedException();
    }

    public BO.Cart Update(BO.Cart cart, int? id, int? amount)
    {
        throw new NotImplementedException();
    }

   public void UpDateOI(BO.Cart cart,int id)
    {
        foreach (var item in cart.Items)
        {
            if(item.ProdectID==id)
            {
                item.Amount++;
                item.TotalPrice+=item.Price;
                return;
            }

        }
    }
}
