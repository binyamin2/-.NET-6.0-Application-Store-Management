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
using static System.Collections.Specialized.BitVector32;


namespace PL.Orders;

/// <summary>
/// Interaction logic for Update_Order_manager.xaml
/// </summary>
public partial class Update_Order_manager : Window
{
    private Orders_VM vm;

    public Update_Order_manager(ViewModel.Orders_VM vm)
    {
        this.vm = vm;
        vm.IsAction = false;
        vm.IsUpdate = false;
        InitializeComponent();

    }

}
