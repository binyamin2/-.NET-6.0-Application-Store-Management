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

    public Orders_VM(BlApi.IBl bl)
    {
        this.bl = bl;
        orders = new ObservableCollection<BO.OrderForList> (bl.Order.GetList());
    }

    public ObservableCollection<BO.OrderForList> orders;

    public ObservableCollection<BO.OrderForList> Orders
    {
        get { return orders; }
        set { Set(ref orders, value); }
    }

    public void ShowUpdate(int id)
    {
        MessageBox.Show("hi");
    }

    public ICommand Update_Item => new RelayCommand<int>(ShowUpdate);

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



    #region Command

    public ICommand AddCommand => new RelayCommand<string>(ShowDetails);
    public ICommand UpdateCommand => new RelayCommand<string>(ShowDetails);

    public ICommand DeleteCommand => new RelayCommand<string>(ShowDetails);
    public ICommand Act => new RelayCommand<Window>(action);

    private void action(Window window)
    {
        try
        {
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
            window.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }




    }

    private void ShowDetails(string str)
    {
        if(str == "Add")
        {
            IsAction = true;
            ButtomText = "Add";
          

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
            ButtomText = "Delete";
        }
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


}
