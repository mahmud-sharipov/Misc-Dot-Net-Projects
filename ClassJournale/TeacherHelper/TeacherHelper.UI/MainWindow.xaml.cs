using System;
using System.Collections.Generic;
using AvalonDock.Themes.MaterilMetro;

namespace TeacherHelper.UI
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new AppViewModel();
        }
    }
}
