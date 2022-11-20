using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AvalonDock.Themes.MaterilMetro
{
    public class MaterilMetroTheme : Xceed.Wpf.AvalonDock.Themes.Theme
    {
        public override Uri GetResourceUri()
        {
            return new Uri(
                "/AvalonDock.Themes.MaterilMetro;component/Theme.xaml",
                UriKind.Relative);
        }
    }
}
