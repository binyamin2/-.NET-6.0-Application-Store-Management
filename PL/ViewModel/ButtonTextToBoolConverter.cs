using System;
using System.Globalization;
using System.Windows.Data;

namespace PL.ViewModel;
/// <summary>
/// class for the convert of text to bool (update or add)
/// </summary>

[ValueConversion(typeof(string), typeof(bool))]
public class ButtonTextToBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value == "Update" ? true : false;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
