using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherHelper.Model;
using AvalonDock.Themes.MaterilMetro;
using System.ComponentModel;
using Xceed.Wpf.AvalonDock.Layout;

namespace TeacherHelper.UI
{
    public class AppViewModel : INotifyPropertyChanged
    {
        readonly MaterilMetroTheme avalonDocTheme = new MaterilMetroTheme();
        public MaterilMetroTheme AvalonDocTheme => avalonDocTheme;

        public EntityContext Context => Global.Context;

        readonly LayoutRoot layoutRoot =new LayoutRoot();
        public LayoutRoot LayoutRoot => layoutRoot;



        public event PropertyChangedEventHandler PropertyChanged;


    }
}
