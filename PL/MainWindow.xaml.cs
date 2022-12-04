﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BlApi.IBl bl = new BlImplementation.Bl();
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
        private void ShowProductsButton_Click(object sender, RoutedEventArgs e) => new PL.Products.ProductForList().Show();

    }
}
