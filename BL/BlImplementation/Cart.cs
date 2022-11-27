using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using Dal;
using DalApi;

namespace BlImplementation;

internal class Cart : BlApi.ICart
{
    private IDal Dal = new DalList();
    /// <summary>
    /// Add product to cart
    /// for catalog and Detail order
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <returns>BO.Cart</returns>
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
    /// <summary>
    /// confirm the order and update the data
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="DetailClient"></param>
    /// <exception cref="BO.WrongCartDeteilsException"></exception>
    public void ConfirmOrder(BO.Cart cart, Tuple<string?, string?, string?> DetailClient)//tuple orser=name,email,addres
    {
        if (string.IsNullOrEmpty(DetailClient.Item1))
            throw new BO.WrongCartDeteilsException("name not valid");
        if (string.IsNullOrEmpty(DetailClient.Item2))
            throw new BO.WrongCartDeteilsException("email not valid");
        if (string.IsNullOrEmpty(DetailClient.Item3))
            throw new BO.WrongCartDeteilsException("adress not valid");
        try
        {
            //building new DO.order
            DO.Order NewO = new DO.Order();
            NewO.CustomerName = DetailClient.Item1;
            NewO.CustomerEmail = DetailClient.Item2;
            NewO.CustomerAdress = DetailClient.Item3;
            NewO.OrderDate = DateTime.Now;
            //building new DO.orderItems
            int? OrderID =Dal.Order.Add(NewO);
            foreach (var item in cart.Items)
            {
                DO.OrderItem NewDOOI = BuildOI(item);
                NewDOOI.OrderID = OrderID ?? throw new NotFoundException("orderitem id null") ;
                Dal.OrderItem.Add(NewDOOI);
            }
            foreach (var item in cart.Items)//for all order item in cart
            {
                foreach (var i in Dal.Product.GetAll())//run in all product to find the 1 who needs update
                {
                    if(item.ProdectID==i.ID)
                    {
                        if (item.Amount>i.InStock)//exeption
                        {
                            throw new BO.WrongCartDeteilsException("the product is out of stock");
                        }
                        //update the data 
                        DO.Product UpdateProduct = new DO.Product();
                        UpdateProduct = i;
                        UpdateProduct.InStock-=item.Amount;
                        Dal.Product.Update(UpdateProduct);
                    }
                }

            }
        }
        catch(Exception ex)
        {
            throw  new WrongCartDeteilsException(ex.Message,ex);
        }

    }
    /// <summary>
    /// update the cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    /// <exception cref="BO.WrongCartDeteilsException"></exception>
    public BO.Cart Update(BO.Cart cart, int id, int amount)
    {
        bool exist = cart.Items.Any(i => i.ProdectID == id);//Check if the product in the cart
        if (!exist)
            throw new BO.WrongCartDeteilsException("the product is not in the cart");
        if (!Dal.Product.GetAll().Any(i => i.ID == id))//Check if the product exist
            throw new BO.WrongCartDeteilsException("the product is not exist");
        int currentInStock=0;
        foreach (var item in Dal.Product.GetAll())//find how match in stock
        {
            if (item.ID == id)
            {
                currentInStock = item.InStock;
                break;
            }
        }
        foreach (var item in cart.Items)//adding/updating the product
        {
            if (item.ProdectID==id)
            {
                if (amount == 0)//delete the product if the amount is 0
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
                else if(item.Amount>amount)//if want smaller amount
                {
                    UpDateOI(cart, id, amount- item.Amount);
                }
                break;
               
            }
            
        }
        return cart;
    }
    /// <summary>
    /// func for update order item
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <param name="amount"></param>
   public void UpDateOI(BO.Cart cart,int id,int amount)
    {
     
        foreach (var item in cart.Items)
        {
            if(item.ProdectID==id)
            {
                item.Amount+=amount;
                item.TotalPrice+=item.Price*amount;
                cart.TotalPrice += item.Price * amount;
                return;
            }

        }
    }
    /// <summary>
    /// func for build new Do.orderitem from Bo.orderitem eccept the order id order item id
    /// </summary>
    /// <param name="BOOI"></param>
    /// <returns></returns>
    public DO.OrderItem BuildOI(BO.OrderItem BOOI)
    {
        DO.OrderItem item = new DO.OrderItem();
        item.ProdectID = BOOI.ProdectID;
        item.Amount = BOOI.Amount;
        item.Price = BOOI.Price;
        return item;
    }
}
