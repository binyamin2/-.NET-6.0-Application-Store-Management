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
using BO;
using PL.ViewModel;

namespace PL.Products;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    /// <summary>
    /// empty ctor
    /// </summary>
    public ProductWindow()
    {
        InitializeComponent();
        ///if change make focus
        IsVisibleChanged += (_, _) =>
        {
            if (Visibility == Visibility.Visible)
                Focus();
        };
    }
    /// <summary>
    /// for move the window with the top line
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Window_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        DragMove();
    }
}