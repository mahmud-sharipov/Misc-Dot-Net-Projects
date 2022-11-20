using Server.Models;
using Server.UIElemetn;
using System;
using System.Collections.Generic;
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
    public partial class HomePage : UserControl, INotifyPropertyChanged
    {
        private AppViewModel model_;
        public event PropertyChangedEventHandler PropertyChanged;

        public HomePage(AppViewModel model)
        {
            model_ = model;
            InitializeComponent();
            DataContext = model_;
        }

        private void NewSupplyBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddEditSupply(model_);
            dialog.Supply.Product = ProductList.SelectedItem as Product;
            dialog.ShowDialog();
            if (dialog.Result == AddEditSupply.YesNoMessageBoxResult.Yes)
            {
                model_.DataProvider.Add(dialog.Supply);
                model_.Supplies.Add(dialog.Supply);
                model_.DataProvider.SaveChanges();
            }
        }

        private void NewInvoiceNtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddEditInvoiceWindow(model_);
            dialog.Invoice.Product = ProductList.SelectedItem as Product;
            dialog.ShowDialog();
            if (dialog.Result == AddEditInvoiceWindow.YesNoMessageBoxResult.Yes)
            {
                if (dialog.Invoice.Amount == 0) return;
                model_.DataProvider.Add(dialog.Invoice);
                model_.Invoices.Add(dialog.Invoice);
                dialog.Invoice.FromWarehouse.Invoices.Add(dialog.Invoice);
                if (dialog.IsCredit.IsChecked == true)
                {
                    var newCredit = new Debtor() { Invoice = dialog.Invoice, Remains = dialog.Invoice.TotalPrice };
                    var dialogCredit = new AddEditDebtor(newCredit);
                    dialogCredit.ShowDialog();
                    if (dialogCredit.Result == YesNoDialogResult.Yes)
                    {
                        model_.DataProvider.Add(dialogCredit.Debtor);
                        model_.Debtors.Add(dialogCredit.Debtor);
                        dialog.Invoice.Debtor = dialogCredit.Debtor;
                    }
                }
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
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Products)));
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            WordDocument doc = new WordDocument("wordTemp/ProductAmountPrice.dot");
            System.Windows.Forms.FolderBrowserDialog dialod = new System.Windows.Forms.FolderBrowserDialog();
            var res = dialod.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(dialod.SelectedPath))
            doc.ReportProductAmountPrice(Products,dialod.SelectedPath);
        }
    }
}
