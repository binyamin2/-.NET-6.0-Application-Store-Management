using PL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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
        public Product_Item(Cart_VM vm,BO.ProductItem pi)
        {
            this.DataContext = vm;
            vm.ProductId=pi.ID;
            vm.ProductName=pi.Name;
            vm.Price = pi.Price;
            vm.Category=pi.Category;
            vm.InStoct=pi.InStock;
            if (vm.InStoct == true)
            {
                vm.InStockText = "Yes";
            }
            vm.IsPi = true;
            vm.IsOi = false;
            this.vm = vm;
            InitializeComponent();
            vm.W = this;
        }

        public Product_Item(Cart_VM vm, BO.OrderItem oi)
        {
            vm.ProductId = oi.ProdectID;
            vm.OiId=oi.ID;
            vm.Amount=oi.Amount;
            vm.Price=oi.Price;
            vm.TotalPrice = oi.TotalPrice;
            vm.ProductName = oi.Name;
            vm.IsOi=true;
            vm.IsPi = false;
            vm.W = this;
            this.vm = vm;
            this.DataContext = vm;
            InitializeComponent();
        }
    }
}
