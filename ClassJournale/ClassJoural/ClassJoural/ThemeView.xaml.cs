using ClassJoural.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClassJoural
{
    /// <summary>
    /// Логика взаимодействия для Theme.xaml
    /// </summary>
    public partial class ThemeView : Window
    {
        public ThemeView(Subject s, DateTime sd,DateTime eD)
        {
            ObservableCollection<Theme> list = new ObservableCollection<Theme>(s.Themes.Where(e => e.Date > sd && e.Date < eD).ToList());
            ListTheme.ItemsSource = list;
            InitializeComponent();
        }
    }
}
