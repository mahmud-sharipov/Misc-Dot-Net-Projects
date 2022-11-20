using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Server.UIElemetn
{
    public partial class ResetPassword : Window
    {
        private AppViewModel model_;

        public ResetPassword(AppViewModel model)
        {
            model_ = model;
            InitializeComponent();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (model_.CurrentUser.Username != OldLoginBox.Text || model_.CurrentUser.Password != OldPasswordBox.Password.GetHashCode().ToString())
            {
                OldErrorBlock.Text = "Неправильный логин или пароль!";
                return;
            }
            OldFormGrid.Visibility = Visibility.Collapsed;
            NewFormGrid.Visibility = Visibility.Visible;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            if (NewLoginBox.Text == string.Empty || NewPasswordBox.Password == string.Empty)
            {
                NewErrorBlock.Text += "Вводите логин и пароль!";
                return;
            }
            model_.CurrentUser.Username = NewLoginBox.Text;
            model_.CurrentUser.Password = NewPasswordBox.Password.GetHashCode().ToString();
            model_.DataProvider.SaveChanges();
            Close();
        }
    }
}
