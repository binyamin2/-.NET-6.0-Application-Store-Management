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
using System.Windows.Shapes;

namespace PL.Carts;


/// <summary>
/// Interaction logic for ListProductItem.xaml
/// </summary>
public partial class ListProductItem : Window
{
    private PL.ViewModel.Cart_VM vm;
    public ListProductItem(ViewModel.Cart_VM vm)
    {
        this.vm = vm;
        this.DataContext = vm;
        InitializeComponent();
    }

    private void OpenProductItem(object sender, MouseButtonEventArgs e) => new PL.Products.Product_Item(vm, (BO.ProductItem)((ListView)sender).SelectedItem).Show();

    private void OpenCart_Click(object sender, RoutedEventArgs e) => new PL.Carts.Cart(vm).Show();

}
