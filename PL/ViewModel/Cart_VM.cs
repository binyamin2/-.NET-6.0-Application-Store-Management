using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModel;

public class Cart_VM: INotifyPropertyChanged
{
    #region OrderWindow
    private int productId;
    public int ProductId
    {
        get { return productId; }
        set { Set(ref productId, value); }
    }

    private string? productName = "";
    public string? ProductName
    {
        get { return productName; }
        set { Set(ref productName, value); }
    }

    private double price;
    public double Price
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

    private bool inStoct = false;
    public bool InStoct
    {
        get { return inStoct; }
        set { Set(ref inStoct, value); }
    }

     private string? inStoctText = "No";
    public string? InStoctText
    {
        get { return inStoctText; }
        set { Set(ref inStoctText, value); }
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
