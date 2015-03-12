using System;
using System.Windows;
using System.Windows.Data;

namespace WebConfigTool.Converters
{
    public class VisibleIfNullOrEmpty : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (String.IsNullOrEmpty(value as string)) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
