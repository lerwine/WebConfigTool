using System;
using System.Windows;
using System.Windows.Data;

namespace WebConfigTool.Converters
{
    public class VisibleIfNullOrWhitespace : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (String.IsNullOrWhiteSpace(value as string)) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
