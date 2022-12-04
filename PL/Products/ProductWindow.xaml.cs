using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BO;

namespace PL.Products;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    BlApi.IBl bl;
    /// <summary>
    /// empty ctor
    /// </summary>
    public ProductWindow(BlApi.IBl bl)
    {
        this.bl = bl;
        InitializeComponent();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        //dont show update button
        UpdateButton.Visibility = Visibility.Collapsed;

    }
    /// <summary>
    /// ctor get id and show update window for the spesific product
    /// </summary>
    /// <param name="id"></param>
    public ProductWindow(BlApi.IBl bl, int id)
    {
        this.bl = bl;
        InitializeComponent();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        //dont show add button
        AddButton.Visibility = Visibility.Collapsed;

        BO.Product product1 = bl.Product.GetForManager(id);

        IDText.Text = product1.ID.ToString();
        CategorySelector.SelectedItem = product1.Category;
        NameText.Text = product1.Name;
        PriceText.Text = product1.Price.ToString(); 
        InStockText.Text = product1.InStock.ToString();


    }

    /// <summary>
    /// button for add product
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonAdd_Click(object sender, RoutedEventArgs e)
    {


        BO.Product? UserProduct = BuildProduct();
        if (UserProduct != null)
        {
            try
            {
                bl.Product.Add(UserProduct);
                MessageBox.Show("The orgen added");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
      
    }
    /// <summary>
    /// button for update product
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {

        BO.Product? UserProduct = BuildProduct();
        if (UserProduct != null)
        {
            try
            {
                bl.Product.Update(UserProduct);
                MessageBox.Show("The orgen updated");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
    }
    //help methods

    /// <summary>
    /// build bo product from the user input
    /// </summary>
    /// <returns></returns>
    private BO.Product? BuildProduct()
    {
        BO.Product UserProduct = new BO.Product();

        int userVal = 0;

        int.TryParse(IDText.Text, out userVal);
        UserProduct.ID = userVal;

        UserProduct.Category = (BO.Category?)CategorySelector.SelectedItem;

        UserProduct.Name = NameText.Text;



        int.TryParse(PriceText.Text, out userVal);
        UserProduct.Price = userVal;



        int.TryParse(InStockText.Text, out userVal);

        UserProduct.InStock = userVal;

   
        return UserProduct;

    }

   
}

//if (int.TryParse(IDText.Text, out userVal))
//{
//    UserProduct.ID = userVal;
//}
//else
//{
//    MessageBox.Show("Hey, we need an int in ID.");
//}