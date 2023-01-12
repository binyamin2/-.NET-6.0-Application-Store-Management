using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using BO;

namespace PL.ViewModel;

public class Orders_VM : INotifyPropertyChanged
{
    BlApi.IBl bl;
    //ctor
    public Orders_VM(BlApi.IBl bl)
    {
        this.bl = bl;
        orders = new ObservableCollection<BO.OrderForList> (bl.Order.GetList());
    }

    public ObservableCollection<BO.OrderForList> orders;//orders list

    public ObservableCollection<BO.OrderForList> Orders
    {
        get { return orders; }
        set { Set(ref orders, value); }
    }


   

    #region OrderWindow
    private bool isUpdate = false;
    public bool IsUpdate
    {
        get { return isUpdate; }
        set { Set(ref isUpdate, value); }
    }
    private bool isAction = false;
    public bool IsAction
    {
        get { return isAction; }
        set { Set(ref isAction, value); }
    }

    private int id;
    public int ID
    {
        get { return id; }
        set { Set(ref id, value); }
    }

    private int productId;
    public int ProductId
    {
        get { return productId; }
        set { Set(ref productId, value); }
    }

    private int amount;
    public int Amount
    {
        get { return amount; }
        set { Set(ref amount, value); }
    }
    private string buttomText = "";

    public string ButtomText
    {
        get { return buttomText; }
        set { Set(ref buttomText, value); }
    }
    private string addText = "Add";

    public string AddText
    {
        get { return addText; }
        set { Set(ref addText, value); }
    }
    private string deleteText = "Delete";

    public string DeleteText
    {
        get { return deleteText; }
        set { Set(ref deleteText, value); }
    }
    private string updateText = "Update";

    public string UpdateText
    {
        get { return updateText; }
        set { Set(ref updateText, value); }
    }
    #endregion
    #region Variable Order Track

    private bool isTrack = false;//if its order tracking ,for display 
    public bool IsTrack
    {
        get { return isTrack; }
        set { Set(ref isTrack, value); }
    }
    private bool isOrderShow = false;//if its order show ,for display 
    public bool IsOrderShow
    {
        get { return isOrderShow; }
        set { Set(ref isOrderShow, value); }
    }

    private string stat = "";//order status
    public string Stat
    {
        get { return stat; }
        set { Set(ref stat, value); }
    }
    private DateTime date;//date of order
    public DateTime Date
    {
        get { return date; }
        set { Set(ref date, value); }
    }
    private BO.OrderTracking ot=new BO.OrderTracking();//order tracking type
    public BO.OrderTracking OT
    {
        get { return ot; }
        set { Set(ref ot, value); }
    }
    private List<Tuple<DateTime?, String?>?>? dateList= new List<Tuple<DateTime?, String?>?>();//the list of the dates of order tracing

    public List<Tuple<DateTime?, String?>?>? DateList
    {
        get { return dateList; }
        set { Set(ref dateList, value); }
    }
    private string? customerName = "";
    public string? CustomerName
    {
        get { return customerName; }
        set { Set(ref customerName, value); }
    }
    private string? customerEmail = "";
    public string? CustomerEmail
    {
        get { return customerEmail; }
        set { Set(ref customerEmail, value); }
    }
    private string? customerAdress = "";
    public string? CustomerAdress
    {
        get { return customerAdress; }
        set { Set(ref customerAdress, value); }
    }
    private OrderStatus? orderStatus ;//the status of the order
    public OrderStatus? OrderStatus
    {
        get { return orderStatus; }
        set { Set(ref orderStatus, value); }
    }
    private double? totalPrice;//total price of order
    public double? TotalPrice
    {
        get { return totalPrice; }
        set { Set(ref totalPrice, value); }
    }
    private List<OrderItem?>? items= new List<OrderItem?>();//the items in the order
    public List<OrderItem?>? Items
    {
        get { return items; }
        set { Set(ref items, value); }
    }




    #endregion


    #region Command
    public ICommand Update_Item => new RelayCommand<int>(ShowUpdate);

    public ICommand AddCommand => new RelayCommand<string>(ShowDetails);//add order display
    public ICommand UpdateCommand => new RelayCommand<string>(ShowDetails);//update order display
    public ICommand ShowTrack => new RelayCommand(TrackShow);//show the tracking details
    public ICommand OrderDetailsShow => new RelayCommand(OrderShow);//to display order details
    public ICommand DeleteCommand => new RelayCommand<string>(ShowDetails);//delete order display
    public ICommand Act => new RelayCommand<Window>(action);//do the wanted action

    private void action(Window window)
    {
        try
        {
            //check what action need to do
            if (ButtomText == "Add")
            {
                bl.Order?.UpdateOIADD(ID, ProductId);
            }
            else if (ButtomText == "Delete")
            {
                bl.Order?.updateOIdelete(ID, ProductId);
            }
            else if (ButtomText == "Update")
            {
                bl.Order?.updateOIAmount(ID,ProductId,Amount);
            }
            Orders = new ObservableCollection<BO.OrderForList>(bl.Order.GetList());
            MessageBox.Show("the organ is " + ButtomText + "Successfully");
            window.Close();//close the window after the action
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }




    }

    private void ShowDetails(string str)
    {
        //show the wanted detauls
        if(str == "Add")
        {
            IsAction = true;
            ButtomText = "Add";
            IsUpdate = false;

        }
        else if (str == "Update")
        {
            IsAction = true;
            IsUpdate = true;
            ButtomText = "Update";

        }
        else if (str == "Delete")
        {
            IsAction= true;
            IsUpdate = false;
            ButtomText = "Delete";
        }
    }
    public void ShowUpdate(int id)
    {
        MessageBox.Show("hi");
    }


    private void TrackShow()//show track details
    {
        OT=bl.Order.OrderTracking(ID);
        DateList=OT.DateList;
        IsTrack = true;
        IsOrderShow = false;
    }
    private void OrderShow()//show order details
    {
        try
        {
            BO.Order order = bl.Order.Get(ID);
            ID= order.ID;
            Items = order.Items;
            CustomerName = order.CustomerName;
            CustomerEmail = order.CustomerEmail;
            CustomerAdress = order.CustomerAdress;
            TotalPrice = order.TotalPrice;
            OrderStatus = order.Status;
            IsOrderShow = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        

    }


    #endregion

    #region PropertyChanged
    //generic set method that create event for every change 
    private void Set<T>(ref T prop, T val, [CallerMemberName] string? name = "")
    {///if was change it invoke to who connect to this event
        if (!prop.Equals(val))
        {
            prop = val;
            PropertyChanged?.Invoke(this, new(name));
        }
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    #endregion


}
