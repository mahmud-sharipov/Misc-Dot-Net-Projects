using Server.Dialogs;
using Server.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Server.Contents
{
    /// <summary>
    /// Логика взаимодействия для OtherPage.xaml
    /// </summary>
    public partial class OtherPage : UserControl
    {
        private AppViewModel model_;

        public OtherPage(AppViewModel model)
        {
            model_ = model;
            InitializeComponent();
        }

        private void UOMsList_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Delete:
                    DaleteUOM_Click(null, null);
                    break;
                case Key.Enter:
                    AddUOM_Click(null, null);
                    break;
                case Key.F1:
                    EditUOM_Click(null, null);
                    break;
            }
        }

        private void EditUOM_Click(object sender, RoutedEventArgs e)
        {
            if (UOMsList.SelectedIndex == -1) return;
            var dialog = new AddEditUOM(UOMsList.SelectedItem as UOM);
            dialog.ShowDialog();
            if (dialog.Result == YesNoDialogResult.Yes)
            {
                model_.UOMs.Add(dialog.UOM);
                model_.DataProvider.Add(dialog.UOM);
                model_.DataProvider.SaveChanges();
            }
        }

        private void DaleteUOM_Click(object sender, RoutedEventArgs e)
        {
            if (UOMsList.SelectedIndex == -1) return;
            var dialog = new YesNo("Вы действительно хотите удалить", "Удаление");
            dialog.ShowDialog();
            if (dialog.Result == YesNo.YesNoMessageBoxResult.Yes)
            {
                var selectedItem = UOMsList.SelectedItem as UOM;
                model_.UOMs.Remove(selectedItem);
                model_.DataProvider.Delete(selectedItem);
                model_.DataProvider.SaveChanges();
            }
        }
        private void AddUOM_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddEditUOM();
            dialog.ShowDialog();
            if (dialog.Result == YesNoDialogResult.Yes)
            {
                model_.UOMs.Add(dialog.UOM);
                model_.DataProvider.Add(dialog.UOM);
                model_.DataProvider.SaveChanges();
            }
        }
    }
}
