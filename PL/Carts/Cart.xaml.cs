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

namespace PL.Carts
{
    /// <summary>
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class Cart : Window
    {
        private PL.ViewModel.Cart_VM vm;
        public Cart(ViewModel.Cart_VM vm)
        {
            this.vm = vm;
            this.DataContext = vm;
            InitializeComponent();
        }

        private void OpenOrderItem(object sender, MouseButtonEventArgs e)=>new PL.Products.Product_Item(vm, (BO.OrderItem)OrderItemListView.SelectedItem).Show();


    }
}
