using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AvalonDock.Themes.MaterilMetro
{
    public class LayoutAnchorable : Xceed.Wpf.AvalonDock.Layout.LayoutAnchorable
    {
        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(LayoutAnchorable));
    }
}
