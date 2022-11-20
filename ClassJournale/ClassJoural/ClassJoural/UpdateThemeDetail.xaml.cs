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
    public partial class UpdateThemeDetail : Window
    {
        public bool res = false;
        public Theme theme;
        Theme testTheme;
        public UpdateThemeDetail(Theme themeD)
        {
            InitializeComponent();
            theme = themeD;
            testTheme = new Theme();
            testTheme.Name = theme.Name;
            testTheme.Description = theme.Description;
            testTheme.IsExam = theme.IsExam;
            foreach (var item in theme.Details.OrderBy(e=>e.Student.FullName))
            {
                var tDeyail = new ThemeDetail() { IsAttend = item.IsAttend, Estimation = item.Estimation, Student = item.Student };
                testTheme.Details.Add(tDeyail);
            }
            DataContext = testTheme;
        }

        private void CanselBtn_Click(object sender, RoutedEventArgs e)
        {
            res = false;
            this.Close();
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            res = true;
            theme.Description = testTheme.Description;
            theme.Name = testTheme.Name;
            theme.IsExam = testTheme.IsExam;
            foreach (var item in testTheme.Details)
            {
                var swap = theme.Details.First(en => en.Student.Id == item.Student.Id);
                swap.IsAttend = item.IsAttend;
                swap.Estimation = item.Estimation;
            }
            Close();
        }
    }
}
