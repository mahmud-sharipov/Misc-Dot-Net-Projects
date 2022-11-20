using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AvalonDock.Themes.MaterilMetro
{
    public class MetroTheme : Xceed.Wpf.AvalonDock.Themes.Theme
    {
        public override Uri GetResourceUri()
        {
            return new Uri(
                "/Theme.Metro;component/Theme.xaml",
                UriKind.Relative);
        }
    }
}
