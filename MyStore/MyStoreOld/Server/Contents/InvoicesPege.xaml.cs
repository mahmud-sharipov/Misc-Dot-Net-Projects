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
    public partial class InvoicesPege : UserControl, INotifyPropertyChanged
    {
        AppViewModel model_;
        public event PropertyChangedEventHandler PropertyChanged;

        public InvoicesPege(AppViewModel model)
        {
            model_ = model;
            InitializeComponent();
            DataContext = this;
        }

        public List<Invoice> Invoices
        {
            get
            {
                return model_.Invoices.Where(e => (IsSortByDate ? e.Date.Date == Date.Date : true) &&
                (TypeFilterText != "" ? e.Product.Type.StartWith(TypeFilterText) : true) &&
                (NameFilterText != "" ? e.Product.Name.StartsWith(NameFilterText) : true) &&
                (NumberFilterText != "" ? e.Product.Number.ToString().StartsWith(NumberFilterText) : true)).ToList();
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Invoices)));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Invoices)));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Invoices)));
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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            WordDocument doc = new WordDocument("wordTemp/EachInvoiceReport.dot");
            System.Windows.Forms.FolderBrowserDialog dialod = new System.Windows.Forms.FolderBrowserDialog();
            var res = dialod.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(dialod.SelectedPath))
                doc.ReportEachInvoise(Invoices, dialod.SelectedPath);
        }

        private bool isSortByDate;
        public bool IsSortByDate
        {
            get { return isSortByDate; }
            set
            {
                isSortByDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSortByDate)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Invoices)));
            }
        }

        private DateTime date = DateTime.Now;
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Date)));
                if (IsSortByDate) PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Invoices)));
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddEditInvoiceWindow(InvoicetList.SelectedItem as Invoice, model_);
            dialog.ShowDialog();
            if (dialog.Result == AddEditInvoiceWindow.YesNoMessageBoxResult.Yes)
            {
                model_.DataProvider.SaveChanges();
            }
            else
            {
                dialog.Invoice.Context.Entry(dialog.Invoice).State = System.Data.Entity.EntityState.Unchanged;
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Invoices)));
        }
    }
}
