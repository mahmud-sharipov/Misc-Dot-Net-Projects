using Server.Dialogs;
using Server.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Server.Contents
{
    public partial class WarehousePage : UserControl, INotifyPropertyChanged
    {
        private AppViewModel model_;
        public event PropertyChangedEventHandler PropertyChanged;

        public WarehousePage(AppViewModel model)
        {
            model_ = model;
            InitializeComponent();
            DataContext = model_;
        }

        private Warehouse currentWarehouse;
        public Warehouse CurrentWarehouse
        {
            get { return currentWarehouse; }
            set
            {
                currentWarehouse = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentWarehouse)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WarehouseProducts)));
            }
        }

        //filter

        public List<WarehouseProduct> WarehouseProducts
        {
            get
            {
                if (CurrentWarehouse == null || CurrentWarehouse.Products == null) return null;
                return CurrentWarehouse.Products.Where(e =>
                    (TypeFilterText != "" ? e.Product.Type.StartWith(TypeFilterText) : true) &&
                    (NameFilterText != "" ? e.Product.Name.StartsWith(NameFilterText) : true) &&
                    (NumberFilterText != "" ? e.Product.Number.ToString().StartsWith(NumberFilterText) : true)
                    ).ToList();
            }
        }

        private string typeFilterText = "";
        public string TypeFilterText
        {
            get { return typeFilterText; }
            set
            {
                typeFilterText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TypeFilterText)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WarehouseProducts)));
            }
        }

        private string nameFilterText = "";
        public string NameFilterText
        {
            get { return nameFilterText; }
            set
            {
                nameFilterText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NameFilterText)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WarehouseProducts)));
            }
        }

        private string numberFilterText = "";
        public string NumberFilterText
        {
            get { return numberFilterText; }
            set
            {
                numberFilterText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumberFilterText)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WarehouseProducts)));
            }
        }

        private void TypeFilter_TextChanged(object sender, string e)
        {
            TypeFilterText = e;
        }

        private void NameFilter_TextChanged(object sender, string e)
        {
            NameFilterText = e;
        }

        private void NumberFilter_TextChanged(object sender, string e)
        {
            NumberFilterText = e;
        }
        //end filter


        private void SuppliersList_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                //case Key.Delete:
                //    Delete_Click(null, null);
                //    break;
                case Key.Enter:
                    Add_Click(null, null);
                    break;
                case Key.F1:
                    Edit_Click(null, null);
                    break;
                case Key.F3:
                    InvoiceReport_Click(null, null);
                    break;
                case Key.F2:
                    SupplyReport_Click(null, null);
                    break;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddEditWarehouse();
            dialog.ShowDialog();
            if (dialog.Result == YesNoDialogResult.Yes)
            {
                model_.Warehouses.Add(dialog.Warehouse);
                model_.DataProvider.Add(dialog.Warehouse);
                foreach (var item in model_.Products)
                {
                    var newWP = model_.DataProvider.Add<WarehouseProduct>();
                    newWP.Warehouse = dialog.Warehouse;
                    newWP.Product = item;
                    dialog.Warehouse.Products.Add(newWP);
                    item.Warehouse.Add(newWP);
                }
                model_.DataProvider.SaveChanges();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (WarehouseList.SelectedIndex == -1) return;
            var dialog = new YesNo("Вы действительно хотите удалить", "Удаление");
            dialog.ShowDialog();
            if (dialog.Result == YesNo.YesNoMessageBoxResult.Yes)
            {
                var sI = WarehouseList.SelectedItem as Warehouse;
                model_.Warehouses.Remove(sI);
                model_.DataProvider.Delete(sI);
                model_.DataProvider.SaveChanges();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (WarehouseList.SelectedIndex == -1) return;
            var dialog = new AddEditWarehouse(WarehouseList.SelectedItem as Warehouse);
            dialog.ShowDialog();
            if (dialog.Result == YesNoDialogResult.Yes)
                model_.DataProvider.SaveChanges();
            else
                dialog.Warehouse.Context.Entry(dialog.Warehouse).State = System.Data.Entity.EntityState.Unchanged;
        }

        private void InvoiceReport_Click(object sender, RoutedEventArgs e)
        {
            if (WarehouseList.SelectedIndex != -1)
            {
                new InvoiceReport(model_, WarehouseList.SelectedItem as Warehouse).ShowDialog();
            }
        }

        private void SupplyReport_Click(object sender, RoutedEventArgs e)
        {
            if (WarehouseList.SelectedIndex != -1)
            {
                new SupplyReport(model_, WarehouseList.SelectedItem as Warehouse).ShowDialog();
            }
        }

        private void Swap_Click(object sender, RoutedEventArgs e)
        {
            if (WarehouseList.SelectedIndex != -1)
            {
                var dialod = new SwapProduct(model_, ProductList.SelectedItem as WarehouseProduct);
                dialod.ShowDialog();
                if (dialod.Result == YesNoDialogResult.Yes)
                {
                    if (dialod.To.Id != dialod.Product.Warehouse.Id)
                    {
                        dialod.Product.Amount -= dialod.Amount;
                        dialod.ToWarehousePeoduct.Amount += dialod.Amount;
                        model_.DataProvider.SaveChanges();
                    }
                }
            }
        }

    }
}
