using System;
using System.Windows;
using System.Windows.Data;

namespace WebConfigTool.Converters
{
    public class VisibleIfFalse : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value == null || (value is bool && (bool)value)) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value == null || (value is Visibility && ((Visibility)value) == Visibility.Collapsed));
        }
    }
}
