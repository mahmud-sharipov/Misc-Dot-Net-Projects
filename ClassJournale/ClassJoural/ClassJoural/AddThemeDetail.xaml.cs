using ClassJoural.Models;
using System;
using System.Collections.Generic;
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
    public partial class AddThemeDetail : Window
    {
        AppViewModel app;
        public Theme theme;
        public bool res = false;
        public AddThemeDetail(AppViewModel appModel)
        {
            InitializeComponent();
            app = appModel;
            theme = new Theme();
            theme.Date = DateTime.Now;
            foreach (var item in app.Students)
            {
                if (item.IsCurrent)
                {
                    var newDetail = new ThemeDetail();
                    newDetail.Student = item;
                    newDetail.Theme = theme;
                    theme.Details.Add(newDetail);
                }
            }
            DataContext = theme;
        }

        private void CanselBtn_Click(object sender, RoutedEventArgs e)
        {
            res = false;
            this.Close();
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            res = true;
            Close();
        }
    }
}
