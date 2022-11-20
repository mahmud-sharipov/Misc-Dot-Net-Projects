using Server.Models;
using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Server
{
    public partial class Authorisation : Window
    {
        DataProvider dp;
        public Authorisation()
        {
            InitializeComponent();
            dp = new DataProvider();
            this.KeyUp += Authorisation_KeyUp;
        }

        private void Authorisation_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.Escape:
                    this.Close();
                    break;
                case System.Windows.Input.Key.Enter:
                    Login(null, null);
                    break;
            }
        }
        private void Login(object sender, RoutedEventArgs e)
        {
            try
            {
                ErrorBlock.Text = string.Empty;
                if (LoginBox.Text == string.Empty || PasswordBox.Password == string.Empty)
                {
                    ErrorBlock.Text += "Вводите логин и пароль!";
                    return;
                }
                var hash = PasswordBox.Password.GetHashCode().ToString();
                var user = dp.GetEntities<User>().SingleOrDefault(ee => ee.Username == LoginBox.Text && ee.Password == "1156371652");

                if (user == null)
                {
                    ErrorBlock.Text += "Неправильный логин или пароль!";
                    return;
                }
                new MainWindow(user, dp).Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
