using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BO;
using DalApi;
using Simulator;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();


        /// <summary>
        /// ctor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// admin actions button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowProductsButton_Click(object sender, RoutedEventArgs e) => new PL.Admin.Admin_Main_Window(bl).Show();

        private void ShowListProductItem(object sender, RoutedEventArgs e) => new PL.Carts.ListProductItem(new ViewModel.Cart_VM(bl) ).Show();

        private void OrderTrack_Click(object sender, RoutedEventArgs e) => new PL.Orders.OrderTracking(new ViewModel.Orders_VM(bl)).Show();

        private void Simulator_Click(object sender, RoutedEventArgs e)
        {
            if (Simulator.Simulator.isAlreadyOpen == false)
            {
                Window simu = new PL.SimulatorWindow();
                simu.Show();
            }
        }

    }
}
