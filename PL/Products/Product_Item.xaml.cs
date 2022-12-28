using PL.ViewModel;
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

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for Product_Item.xaml
    /// </summary>
    public partial class Product_Item : Window
    {
        private Cart_VM vm;
        public Product_Item(Cart_VM vm)
        {
            this.vm = vm;
            InitializeComponent();
        }
    }
}
