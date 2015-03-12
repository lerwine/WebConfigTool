using System;
using System.Windows;
using System.Windows.Data;

namespace WebConfigTool.Converters
{
    public class CollapsedIfNullOrWhitespace : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (String.IsNullOrWhiteSpace(value as string)) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
