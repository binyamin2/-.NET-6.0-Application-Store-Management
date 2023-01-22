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


/// <summary>
/// MVVM for window of product
/// </summary>
public class ProductForListVM : INotifyPropertyChanged
{
    BlApi.IBl bl;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="bl"></param>
    public ProductForListVM(BlApi.IBl bl)
    {
        this.bl = bl;
        ///list of product
        products = new(bl.Product.GetList());
        /// productsCollectionFilter class that contain the Observable and make filter
        productsCollectionFilter = new();
        ProductsCollectionFilter.Source = products;
        ProductsCollectionFilter.Filter += ProductsCollectionFilter_Filter;
        ///sort
        ProductsCollectionFilter.SortDescriptions.Add(new("ID", ListSortDirection.Ascending));
        CategoryUI = CategoryUI.All;

        //for product window
        ProductWindowVisible = false;
    }


    #region ProductForList window

    /// <summary>
    /// The filter for CollectionViewSource "productsCollectionFilter"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    private void ProductsCollectionFilter_Filter(object sender, FilterEventArgs e)
    {
        if (CategoryUI == CategoryUI.All)
        {
            e.Accepted = true;
            return;
        }
        if (e.Item is BO.ProductForList product)
            e.Accepted = (int)product.Category == (int)CategoryUI;
        else///only for safe
            e.Accepted = true;
    }

    /// <summary>
    /// ObservableCollection<ProductForList> products for list 
    /// </summary>
    private ObservableCollection<ProductForList> products;

    public ObservableCollection<ProductForList> Products
    {
        get { return products; }
        set { Set(ref products, value); }
    }

    /// <summary>
    /// BO.CategoryUI for select in comboBox in Filter (with all)
    /// </summary>
    private BO.CategoryUI categoryUI;

    public BO.CategoryUI CategoryUI
    {
        get => categoryUI;
        set
        {
            Set(ref categoryUI, value);
            ProductsCollectionFilter.View.Refresh();
        }
    }
    /// <summary>
    /// for list in combobox CategoryUI
    /// </summary>
    public IEnumerable<BO.CategoryUI> CategoriesUI => Enum.GetValues(typeof(BO.CategoryUI)).Cast<BO.CategoryUI>();

    /// <summary>
    /// ///  CollectionViewSource "productsCollectionFilter" class that contain the Observable and make filter
    /// </summary>

    private CollectionViewSource productsCollectionFilter;
    public CollectionViewSource ProductsCollectionFilter
    {
        get { return productsCollectionFilter; }
        set { Set(ref productsCollectionFilter, value); }
    }

    #endregion

    #region ProductWindow


    ///ALL the propartychange for ProductWindow 

    public IEnumerable<BO.Category> Categories => Enum.GetValues(typeof(BO.Category)).Cast<BO.Category>();


    private bool productWindowVisible;
    public bool ProductWindowVisible
    {
        get { return productWindowVisible; }
        set { Set(ref productWindowVisible, value); }
    }

    private bool deleteVisible;
    public bool DeleteVisible
    {
        get { return deleteVisible; }
        set { Set(ref deleteVisible, value); }
    }
    private int id;
    public int ID
    {
        get { return id; }
        set { Set(ref id, value); }
    }
   
    /// <summary>
    /// BO.CategoryUI for add and update button
    /// </summary>
    private BO.Category? category;
    public BO.Category? Category
    {
        get { return category; }
        set { Set(ref category, value); }
    }

    private string? name = "";
    public string? Name
    {
        get { return name; }
        set { Set(ref name, value); }
    }

    private double price;
    public double Price
    {
        get { return price; }
        set { Set(ref price, value); }
    }

    private int inStock;
    public int InStock
    {
        get { return inStock; }
        set { Set(ref inStock, value); }
    }

    private string buttonText = "";
    public string ButtonText
    {
        get { return buttonText; }
        set { Set(ref buttonText, value); }
    }
    #endregion
    #region function

    /// <summary>
    /// the fuction of the windows
    /// </summary>
    /// 


    ///for show product window for add
    public void ShowAdd()
    {
        ProductWindowVisible = true;
        ButtonText = "Add";
        ID = 0;
        Name = "";
        InStock = 0;
        Price = 0;
        Category = BO.Category.Elegant;
        DeleteVisible= false;
    }
    /// <summary>
    /// ///for show product window for update
    /// </summary>
    /// <param name="id"></param>
    public void ShowUpdate(int id)
    {
        var product = bl.Product.GetForManager(id);
        Category = product.Category;
        Price = product.Price;
        Name = product.Name;
        InStock = product.InStock;
        ID = id;
        ProductWindowVisible = true;
        ButtonText = "Update";
        DeleteVisible = true;
    }


    /// <summary>
    /// for button in product window
    /// </summary>
    public void AddOrUpdate()
    {
        switch (ButtonText)
        {
            case "Add":
                AddProduct();
                break;
            case "Update":
                UpdateProduct();
                break;

        }

    }
    /// <summary>
    /// update the product
    /// </summary>
    private void UpdateProduct()
    {
        // AddOrUpdate to UI
        var product = new ProductForList()
        {
            Category = Category,
            ID = ID,
            Name = Name,
            Price = Price
        };
        
        // AddOrUpdate to database
        try 
        { 
            bl.Product.Update(new()
            {
                Category = Category,
                ID = ID,
                InStock = InStock,
                Name = Name,
                Price = Price
            });
            int index = products.IndexOf(products.First(product => product.ID == ID));
            products[index] = product;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
        ProductWindowVisible = false;
    }
    /// <summary>
    /// add the product
    /// </summary>
    private void AddProduct()
    {
        // AddOrUpdate to UI
        var product = new ProductForList()
        {
            Category = Category,
            ID = ID,
            Name = Name,
            Price = Price
        };
        
        // AddOrUpdate to database
        try
        {
            bl.Product.Add(new()
            {
                Category = Category,
                ID = ID,
                InStock = InStock,
                Name = Name,
                Price = Price
            });
            products.Add(product);
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }

        ProductWindowVisible = false;
    }
    private void DeleteProduct()
    {
        try
        {
            bl.Product.Delete(ID);
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
        ProductWindowVisible = false;
    }
    /// <summary>
    /// The RelayCommand that heritage from command and play the function
    /// </summary>
    public ICommand AddCommand => new RelayCommand(ShowAdd);
    public ICommand UpdateCommand => new RelayCommand<int>(ShowUpdate);
    public ICommand AddAndUpdateCommand => new RelayCommand(AddOrUpdate);
    public ICommand DeleteCommand => new RelayCommand(DeleteProduct);
    /// <summary>
    /// hides the window
    /// </summary>
    public ICommand CancelCommand => new RelayCommand(() => ProductWindowVisible = false);

    #endregion
    #region PropertyChanged
    //generic set method that create event for every change 
    private void Set<T>(ref T prop, T val, [CallerMemberName] string? name = "")
    {
        ///if was change it invoke to who connect to this event
        if (!prop.Equals(val))
        {
            prop = val;
            PropertyChanged?.Invoke(this, new(name));
        }
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    #endregion
}
