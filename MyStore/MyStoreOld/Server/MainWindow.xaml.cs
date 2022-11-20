using Server.Models;
using Server.UIElemetn;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Server
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        AppViewModel appViewModel;
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow(User currentUser, DataProvider dp)
        {
            InitializeComponent();
            AppViewModel = new AppViewModel(dp) { CurrentUser = currentUser };
            DataContext = AppViewModel;
            InitEvents();
        }

        public AppViewModel AppViewModel
        {
            get { return appViewModel; }
            set { appViewModel = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AppViewModel))); }
        }

        private void InitEvents()
        {
            exitBtn.Click += delegate { this.Close(); };
            this.Closing += delegate
            {
                try
                {
                    appViewModel.SaveChanges();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            };
        }

        private void ItemsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MenuToggleButton.IsChecked = false;
        }

        private void ChangeUser(object sender, RoutedEventArgs e)
        {
            Close();
            System.Windows.Forms.Application.Restart();
        }

        private void ResetPessword_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ResetPassword(AppViewModel);
            dialog.ShowDialog();
        }
    }
}
