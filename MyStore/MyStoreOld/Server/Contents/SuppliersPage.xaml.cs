using Server.Dialogs;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class SuppliersPage : UserControl, INotifyPropertyChanged
    {
        private AppViewModel model_;
        public event PropertyChangedEventHandler PropertyChanged;

        public SuppliersPage(AppViewModel model)
        {
            model_ = model;
            Supplies = model_.Supplies.ToList();
            InitializeComponent();
            DataContext = model_;
        }

        private List<Supply> supplies;
        public List<Supply> Supplies
        {
            get { return supplies; }
            set { supplies = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Supplies))); }
        }

        private Supplier currentSupply;
        public Supplier CurrentSupplier
        {
            get { return currentSupply; }
            set
            {
                currentSupply = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentSupplier)));
                if (value != null) Supplies = value.Supplies.ToList();
                else Supplies = model_.Supplies.ToList();
            }
        }

        private void EditSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (SuppliersList.SelectedIndex != -1)
            {
                var dialod = new AddEditSupplier(SuppliersList.SelectedItem as Supplier);
                dialod.ShowDialog();
                if (dialod.Result == YesNoDialogResult.Yes)
                {
                    model_.DataProvider.SaveChanges();
                }
                else
                {
                    dialod.user_.Context.Entry(dialod.user_).State = System.Data.Entity.EntityState.Unchanged;
                }
            }
        }

        private void DeleteSpplier_Click(object sender, RoutedEventArgs e)
        {
            if (SuppliersList.SelectedIndex == -1) return;
            var dialog = new YesNo("Вы действительно хотите удалить этого поставщика", "Удаление поставщика");
            dialog.ShowDialog();
            if (dialog.Result == YesNo.YesNoMessageBoxResult.Yes)
            {
                var sI = SuppliersList.SelectedItem as Supplier;
                model_.Suppliers.Remove(sI);
                model_.DataProvider.Delete(sI);
                model_.DataProvider.SaveChanges();
            }
        }

        private void AddSpplier_Click(object sender, RoutedEventArgs e)
        {
            var dialod = new AddEditSupplier();
            dialod.ShowDialog();
            if (dialod.Result == YesNoDialogResult.Yes)
            {
                model_.DataProvider.Add(dialod.user_);
                model_.Suppliers.Add(dialod.user_);
                model_.DataProvider.SaveChanges();
            }
        }

        private void SuppliersList_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                //case Key.Delete:
                //    DeleteSpplier_Click(null, null);
                //    break;
                case Key.Enter:
                    AddSpplier_Click(null, null);
                    break;
                case Key.F1:
                    EditSupplier_Click(null, null);
                    break;
                case Key.F2:
                    Report_Click(null, null);
                    break;
            }
        }

        private void ResetListSelectItem_Click(object sender, RoutedEventArgs e)
        {
            SuppliersList.SelectedIndex = -1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddEditSupply(ProductList.SelectedItem as Supply, model_, true);
            dialog.ShowDialog();
            if (dialog.Result == AddEditSupply.YesNoMessageBoxResult.Yes)
            {
                model_.DataProvider.SaveChanges();
            }
            else
            {
                dialog.Supply.Context.Entry(dialog.Supply).State = System.Data.Entity.EntityState.Unchanged;
            }
        }

        private void ViewInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ResetRepotrList_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            if (SuppliersList.SelectedIndex == -1) return;
            var dialog = new SupplyReport(model_, SuppliersList.SelectedItem as Supplier);
            dialog.ShowDialog();
        }
    }
}
