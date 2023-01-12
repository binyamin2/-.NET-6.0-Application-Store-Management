using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using PL.ViewModel;

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductForList.xaml
    /// </summary>
    public partial class ProductForList : Window
    {
        private ProductForListVM vm;

        /// <summary>
        /// the window all the time is exists
        /// </summary>
        private ProductWindow productWindow;
        public ProductWindow ProductWindow => productWindow;

        /// <summary>
        /// empty ctor
        /// </summary>
        public ProductForList(ViewModel.ProductForListVM vm)
        {
            InitializeComponent();

            this.vm = vm;
            DataContext = vm;
            productWindow = new() { DataContext = vm };
            //if change make focus
            productWindow.IsVisibleChanged += (s, e) =>
            {
                if (productWindow.Visibility == Visibility.Collapsed)
                    this.Focus();
            };
        }
    }
}
