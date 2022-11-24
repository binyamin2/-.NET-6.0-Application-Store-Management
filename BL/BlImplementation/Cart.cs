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
                    if (item.ID == id)
                    {
                        product.ID = item.ID;
                        product.Name = item.Name;
                        product.Price = item.Price;
                        product.InStock = item.InStock;
                        product.Category = (BO.Category)item.Category;
                        break;
                    }
                }
                BO.OrderItem newOI = new BO.OrderItem();
                newOI.Price = product.Price;
                newOI.Name = product.Name;
                newOI.ProdectID = product.ID;
                newOI.Amount = 1;
                newOI.TotalPrice = newOI.Price;
                cart.Items.Add(newOI);
                cart.TotalPrice += newOI.TotalPrice;
                return cart;

            }
            else
                throw new BO.WrongCartDeteilsException("the product is not exist");
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
        UpDateOI(cart, id,1);
        return cart;
      
    }

    public void ConfirmOrder(BO.Cart cart, Tuple<string?, string?, string?> DetailClient)
    { 
        throw new NotImplementedException();
    }

    public BO.Cart Update(BO.Cart cart, int id, int amount)
    {
        bool exist = cart.Items.Any(i => i.ProdectID == id);
        if (!exist)
            throw new BO.WrongCartDeteilsException("the product is not in the cart");
        if (!Dal.Product.GetAll().Any(i => i.ID == id))
            throw new BO.WrongCartDeteilsException("the product is not exist");
        int currentInStock=0;
        foreach (var item in Dal.Product.GetAll())
        {
            if (item.ID == id)
            {
                currentInStock = item.InStock;
                break;
            }
        }
        foreach (var item in cart.Items)
        {
            if (item.ProdectID==id)
            {
                if (amount == 0)
                {
                    cart.TotalPrice-=item.TotalPrice;
                    cart.Items.Remove(item);
                    return cart;
                }
                else if (item.Amount<amount)//if want biger amount
                {
                    if((amount - item.Amount) >currentInStock)//check if have enough product to add
                    {
                        throw new BO.WrongCartDeteilsException("there no enough product in stock");
                    }
                    UpDateOI(cart,id, amount - item.Amount);
                }
                else if(item.Amount>amount)
                {
                    UpDateOI(cart, id, amount- item.Amount);
                }
                break;
               
            }
            
        }
        return cart;
    }

   public void UpDateOI(BO.Cart cart,int id,int amount)
    {
     
        foreach (var item in cart.Items)
        {
            if(item.ProdectID==id)
            {
                item.Amount+=amount;
                item.TotalPrice+=item.Price*amount;
                return;
            }

        }
    }
}
