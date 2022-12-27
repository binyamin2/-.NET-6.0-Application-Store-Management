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
using PL.ViewModel;
using BlImplementation;

namespace PL.Orders;

/// <summary>
/// Interaction logic for Orders_for_list_window.xaml
/// </summary>
public partial class Orders_for_list_window : Window
{
    private Orders_VM vm;
    public Orders_for_list_window(ViewModel.Orders_VM vm)
    {
        this.vm = vm;
        DataContext = vm;
        InitializeComponent();
    }

    private void update_item(object sender, MouseButtonEventArgs e)
    {
        PL.Orders.Update_Order_manager update = new PL.Orders.Update_Order_manager(vm) { DataContext = vm };
        update.Show();
    }

}
        
