using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace AvalonDock.Themes.MaterilMetro
{
    public class TextWrapConvenrter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = value as string;
            int maxLenght = int.Parse(parameter?.ToString() ?? "30");
            if (text.Length > maxLenght)
                return text.Substring(0, (maxLenght / 2)) + "..." + text.Substring(text.Length - (maxLenght / 2));
            return text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
    }
}
