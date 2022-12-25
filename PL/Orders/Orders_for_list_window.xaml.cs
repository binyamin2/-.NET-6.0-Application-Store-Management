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
}
