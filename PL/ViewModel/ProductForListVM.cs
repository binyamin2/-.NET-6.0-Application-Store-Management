using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using BO;

namespace PL.ViewModel;

public class ProductForListVM : INotifyPropertyChanged
{
    BlApi.IBl bl;

    public ProductForListVM(BlApi.IBl bl)
    {
        this.bl = bl;
        products = new(bl.Product.GetList());
        productsCollectionFilter = new();
        ProductsCollectionFilter.Source = products;
        ProductsCollectionFilter.Filter += ProductsCollectionFilter_Filter;
        ProductsCollectionFilter.SortDescriptions.Add(new("ID", ListSortDirection.Ascending));
        CategoryUI = CategoryUI.All;
        ProductWindowVisible = false;
    }

    private void ProductsCollectionFilter_Filter(object sender, FilterEventArgs e)
    {
        if (CategoryUI == CategoryUI.All)
        {
            e.Accepted = true;
            return;
        }
        if (e.Item is BO.ProductForList product)
            e.Accepted = (int)product.Category == (int)CategoryUI;
        else
            e.Accepted = true;
    }

    // V1
    //private ObservableCollection<ProductForList> products;

    //public ObservableCollection<ProductForList> Products
    //{
    //    get { return products; }

    //    set
    //    {
    //        if (products != value)
    //        {
    //            products = value;
    //            PropertyChanged?.Invoke(this, new("Products"));
    //        }
    //    }
    //}

    //V2
    private ObservableCollection<ProductForList> products;

    public ObservableCollection<ProductForList> Products
    {
        get { return products; }
        set { Set(ref products, value); }
    }

    /// <summary>
    /// BO.CategoryUI for comboBox in Filter
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

    public IEnumerable<BO.CategoryUI> CategoriesUI => Enum.GetValues(typeof(BO.CategoryUI)).Cast<BO.CategoryUI>();
    public IEnumerable<BO.Category> Categories => Enum.GetValues(typeof(BO.Category)).Cast<BO.Category>();

    private CollectionViewSource productsCollectionFilter;
    public CollectionViewSource ProductsCollectionFilter
    {
        get { return productsCollectionFilter; }
        set { Set(ref productsCollectionFilter, value); }
    }


    #region ProductWindow
    private bool productWindowVisible;
    public bool ProductWindowVisible
    {
        get { return productWindowVisible; }
        set { Set(ref productWindowVisible, value); }
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

    public void ShowAdd()
    {
        ProductWindowVisible = true;
        ButtonText = "Add";
        ID = 0;
        Name = "";
        InStock = 0;
        Price = 0;
        Category = BO.Category.Elegant;
    }

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
    }

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
        int index = products.IndexOf(products.First(product => product.ID == ID));
        products[index] = product;
        // AddOrUpdate to database
        bl.Product.Update(new()
        {
            Category = Category,
            ID = ID,
            InStock = InStock,
            Name = Name,
            Price = Price
        });
        ProductWindowVisible = false;
    }

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
        products.Add(product);
        // AddOrUpdate to database
        bl.Product.Add(new()
        {
            Category = Category,
            ID = ID,
            InStock = InStock,
            Name = Name,
            Price = Price
        });
        ProductWindowVisible = false;
    }

    public ICommand AddCommand => new RelayCommand(ShowAdd);
    public ICommand UpdateCommand => new RelayCommand<int>(ShowUpdate);
    public ICommand AddAndUpdateCommand => new RelayCommand(AddOrUpdate);
    public ICommand CancelCommand => new RelayCommand(() => ProductWindowVisible = false);


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
