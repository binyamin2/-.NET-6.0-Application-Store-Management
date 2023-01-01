﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace PL.ViewModel;

public class Cart_VM: INotifyPropertyChanged
{
    BlApi.IBl bl;
    public Cart_VM(BlApi.IBl bl)
    {
        this.bl = bl;
        listproductItems = new ObservableCollection<BO.ProductItem>(bl.Product.GetListProductItems());
        grouplistproductItems = new ObservableCollection<Group>(this.MakeGrouping());
            }
    BO.Cart cart=new BO.Cart();
    

    #region command 
    public ICommand DeleteCommand => new RelayCommand(DelOi);
    public ICommand AddAction => new RelayCommand(AddPi);
    public ICommand UpdateShow => new RelayCommand(ShowUpdate);
    public ICommand UpdateAction => new RelayCommand(UpdateOi);

    public ICommand OrderByGrouping => new RelayCommand(orderByGrouping);

    public ICommand OrderByRegular => new RelayCommand(orderByRegular);

    private void orderByRegular()
    {
        IsRegularOrder = true;
        IsGrouping = false;
    }

    private void orderByGrouping()
    {
        IsRegularOrder = false;
        IsGrouping = true;
    }
    private void DelOi()
    {
        try
        {
            cart = bl.Cart.Update(cart, ProductId, 0);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        W.Close();
    }

    private void AddPi()
    {
        try
        {
            cart = bl.Cart.Add(cart, ProductId);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        W.Close();
    }

    private void ShowUpdate()
    {
        IsUpdate = true;
    }

    private void UpdateOi()
    {
        try
        {
            cart = bl.Cart.Update(cart, ProductId, NewAmount);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        W.Close();
    }
    #endregion
    #region Variable ProducyItemWindoe



    private Window w = new Window();
    public Window W
    {
        get { return w; }
        set { Set(ref w, value); }
    }
    private int productId;
    public int ProductId
    {
        get { return productId; }
        set { Set(ref productId, value); }
    }

    private int oiId;
    public int OiId//order item id
    {
        get { return oiId; }
        set { Set(ref oiId, value); }
    }

    private string? productName = "";
    public string? ProductName
    {
        get { return productName; }
        set { Set(ref productName, value); }
    }

    private double? price;
    public double? Price
    {
        get { return price; }
        set { Set(ref price, value); }
    }
    private BO.Category? category;
    public BO.Category? Category
    {
        get { return category; }
        set { Set(ref category, value); }
    }

    private int amount;
    public int Amount//order item amount
    {
        get { return amount; }
        set { Set(ref amount, value); }
    }

    private double totalPrice;
    public double TotalPrice//order item total price
    {
        get { return totalPrice; }
        set { Set(ref totalPrice, value); }
    }

    private int newAmount;
    public int NewAmount//order item new amount to apdate
    {
        get { return newAmount; }
        set { Set(ref newAmount, value); }
    }

    private bool inStoct = false;
    public bool InStoct
    {
        get { return inStoct; }
        set { Set(ref inStoct, value); }
    }

     private string? inStockText = "No";
    public string? InStockText
    {
        get { return inStockText; }
        set { Set(ref inStockText, value); }
    }

    private bool isUpdate = false;
    public bool IsUpdate//if want to update amount
    {
        get { return isUpdate; }
        set { Set(ref isUpdate, value); }
    }
    private bool isPi = false;
    public bool IsPi//if product item
    {
        get { return isPi; }
        set { Set(ref isPi, value); }
    }
    private bool isOi = false;
    public bool IsOi//if order item
    {
        get { return isOi; }
        set { Set(ref isOi, value); }
    }
    #endregion
    #region Variable ListProducyItemWindoe

    public ObservableCollection<Group> grouplistproductItems;

    public ObservableCollection<Group> GroupListProductItems
    {
        get { return grouplistproductItems; }
        set { Set(ref grouplistproductItems, value); }
    }

    public ObservableCollection<BO.ProductItem> listproductItems;

    public ObservableCollection<BO.ProductItem> ListProductItems
    {
        get { return listproductItems; }
        set { Set(ref listproductItems, value); }
    }

    private bool isGrouping = false;
    public bool IsGrouping
    {
        get { return isGrouping; }
        set { Set(ref isGrouping, value); }
    }

    private bool isRegularOrder = true;
    public bool IsRegularOrder
    {
        get { return isRegularOrder; }
        set { Set(ref isRegularOrder, value); }
    }

    #endregion

    #region PropertyChanged
    private void Set<T>(ref T prop, T val, [CallerMemberName] string? name = "")
    {
        if (!prop.Equals(val))
        {
            prop = val;
            PropertyChanged?.Invoke(this, new(name));
        }
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    #endregion

    #region Help Class

    public class Group
    {
        public BO.Category? TitleGroup  { get; set; }
        public List<BO.ProductItem> GroupProductItems { get; set; }
        public override string ToString() => $@"
        {TitleGroup}
            ddddddddd" ;
    }
    #endregion

    private IEnumerable<Group> MakeGrouping()
    {
        var groups = bl.Product.GetListProductItems().GroupBy(ProductI => ProductI.Category);
        List<Group> ListGroup = new List<Group>();
        foreach (var group in groups)
        {
            ListGroup.Add(new Group()
            {
                TitleGroup = group.Key,
                GroupProductItems = group.ToList()
            });
        }

        return ListGroup;

    }

}

