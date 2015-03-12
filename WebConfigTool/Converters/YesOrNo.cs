using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WebConfigTool.Converters
{
    public class YesOrNo : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value != null && value is bool) ? (((bool)value) ? "Yes" : "No") : "n/a";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value != null && value is string) ? (((value as string) == "Yes") ? true as bool? : (((value as string) == "No") ? false as bool? : null)) : null;
        }
    }
}