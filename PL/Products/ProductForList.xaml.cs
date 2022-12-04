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

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductForList.xaml
    /// </summary>
    public partial class ProductForList : Window
    {
        BlApi.IBl bl ;
        /// <summary>
        /// empty ctor
        /// </summary>
        public ProductForList(BlApi.IBl bl)
        {
           
            this.bl = bl;

            InitializeComponent();

            PruductsListView.ItemsSource = bl.Product.GetList();
            
            catagorySelector.ItemsSource = Enum.GetValues(typeof(BO.CategoryUI));

            catagorySelector.Text = BO.CategoryUI.All.ToString();


        }
        /// <summary>
        /// select category in combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void catagorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            PruductsListView.ItemsSource = bl.Product.GetCategory((BO.CategoryUI)catagorySelector.SelectedItem);

        }

        /// <summary>
        /// button for add product 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, RoutedEventArgs e) => new PL.Products.ProductWindow(bl).Show();

        /// <summary>
        /// button for update spesific product 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_DoubleClick(object sender, MouseButtonEventArgs e) => new PL.Products.ProductWindow(bl,((BO.ProductForList)PruductsListView.SelectedItem).ID).Show();

    }
}
