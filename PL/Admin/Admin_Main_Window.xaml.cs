using BlImplementation;
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

namespace PL.Admin
{
    /// <summary>
    /// Interaction logic for Admin_Main_Window.xaml
    /// </summary>
    public partial class Admin_Main_Window : Window
    {
        BlApi.IBl bl;
        public Admin_Main_Window(BlApi.IBl bl)
        {
            this.bl = bl;
            InitializeComponent();
        }

        private void ShowProductsButton_Click(object sender, RoutedEventArgs e) => new PL.Products.ProductForList(new ViewModel.ProductForListVM(bl)).Show();

        private void Show_Order_Button_Click(object sender, RoutedEventArgs e) => new PL.Orders.Orders_for_list_window(new ViewModel.Orders_VM(bl)).Show();
       
    }
}
