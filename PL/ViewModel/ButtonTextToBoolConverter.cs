using System;
using System.Globalization;
using System.Windows.Data;

namespace PL.ViewModel;

[ValueConversion(typeof(string), typeof(bool))]
public class ButtonTextToBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value == "Update" ? true : false;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
