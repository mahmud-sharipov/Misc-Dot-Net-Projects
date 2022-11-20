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
    public partial class ProductsPage : UserControl, INotifyPropertyChanged
    {
        AppViewModel model_;
        public event PropertyChangedEventHandler PropertyChanged;

        public ProductsPage(AppViewModel model)
        {
            model_ = model;
            InitializeComponent();
            DataContext = this;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddEditProductWindow(model_.DataProvider);
            dialog.ShowDialog();
            if (dialog.Result == AddEditProductWindow.YesNoMessageBoxResult.Yes)
            {
                model_.DataProvider.Add(dialog.Product);
                model_.Products.Add(dialog.Product);
                foreach (var item in model_.Warehouses)
                {
                    var newWP = model_.DataProvider.Add<WarehouseProduct>();
                    newWP.Warehouse = item;
                    newWP.Product = dialog.Product;
                    dialog.Product.Warehouse.Add(newWP);
                    item.Products.Add(newWP);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Products)));
                }
                model_.DataProvider.SaveChanges();
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ProductList.SelectedIndex != -1)
            {
                var dialod = new AddEditProductWindow(ProductList.SelectedItem as Product, model_.DataProvider, true);
                dialod.ShowDialog();
                if (dialod.Result == AddEditProductWindow.YesNoMessageBoxResult.Yes)
                {
                    model_.DataProvider.SaveChanges();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Products)));
                }
                else
                {
                    dialod.Product.Context.Entry(dialod.Product).State = System.Data.Entity.EntityState.Unchanged;
                }
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new YesNo("Вы действительно хотите удалить этот товар", "Удаление товара");
            dialog.ShowDialog();
            if (dialog.Result == YesNo.YesNoMessageBoxResult.Yes)
            {
                var s = ProductList.SelectedItem as Product;
                model_.Products.Remove(s);
                model_.DataProvider.Delete(s);
                model_.DataProvider.SaveChanges();
            }

        }

        public List<Product> Products
        {
            get
            {
                return model_.Products.Where(e =>
                (TypeFilterText != "" ? e.Type.StartWith(TypeFilterText) : true) &&
                (NameFilterText != "" ? e.Name.StartsWith(NameFilterText) : true) &&
                (NumberFilterText != "" ? e.Number.ToString().StartsWith(NumberFilterText) : true)
                )
                .ToList();
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Products)));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Products)));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Products)));
            }
        }

        private void NumberFilter_TextChanged(object sender, string e)
        {
            NumberFilterText = e;
        }

        private void NameFilter_TextChanged(object sender, string e)
        {
            NameFilterText = e;
        }

        private void TypeFilter_TextChanged(object sender, string e)
        {
            TypeFilterText = e;
        }
    }
}
