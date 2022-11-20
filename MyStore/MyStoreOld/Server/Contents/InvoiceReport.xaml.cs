using Server.Models;
using Server.UIElemetn;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Server.Contents
{
    public partial class InvoiceReport : Window, INotifyPropertyChanged
    {
        private AppViewModel model_;
        public event PropertyChangedEventHandler PropertyChanged;
        ObservableCollection<UIElemetn.InvoiceReport> reports = new ObservableCollection<UIElemetn.InvoiceReport>();
        bool isOne = false;

        public InvoiceReport(AppViewModel model)
        {
            model_ = model;
            foreach (var item in model_.Products)
                reports.Add(new UIElemetn.InvoiceReport() { Product = item, Warehouse = CurrentWarehouse, EqualDate = IsSortByDate, Date = Date });
            InitializeComponent();
            FilterContent.DataContext = model_;
            DataContext = this;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Reports)));
        }

        public InvoiceReport(AppViewModel model, Product product)
        {
            model_ = model;
            isOne = true;
            reports.Add(new UIElemetn.InvoiceReport() { Product = product, Warehouse = CurrentWarehouse, EqualDate = IsSortByDate, Date = Date });
            InitializeComponent();
            FilterContent.DataContext = model_;
            DataContext = this;
        }

        public InvoiceReport(AppViewModel model, Warehouse warehouse) : this(model) { CurrentWarehouse = warehouse; }
        private Warehouse currentWarehouse;
        public Warehouse CurrentWarehouse
        {
            get { return currentWarehouse; }
            set
            {
                currentWarehouse = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentWarehouse)));
                foreach (var item in reports)
                    item.Warehouse = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Reports)));
            }
        }

        public List<UIElemetn.InvoiceReport> Reports
        {
            get { return (VisibilEmptyItem ? reports.ToList() : reports.Where(e => e.Amount != 0).ToList()); }
        }


        private bool visibilEmptyItem;
        public bool VisibilEmptyItem
        {
            get { return visibilEmptyItem; }
            set
            {
                visibilEmptyItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VisibilEmptyItem)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Reports)));
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
                if (IsSortByDate)
                    foreach (var item in reports)
                        item.Date = value;
            }
        }


        private bool isSortByDate = false;
        public bool IsSortByDate
        {
            get { return isSortByDate; }
            set
            {
                isSortByDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSortByDate)));
                foreach (var item in reports)
                    item.EqualDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Reports)));
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WarehouseList.SelectedIndex = -1;
            if (isOne)
            {
                reports.Clear();
                foreach (var item in model_.Products)
                    reports.Add(new UIElemetn.InvoiceReport() { Product = item, Warehouse = CurrentWarehouse, EqualDate = IsSortByDate, Date = Date });
                isOne = false;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Reports)));
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            WordDocument doc = new WordDocument("wordTemp/SupplyReport.dot");
            FolderBrowserDialog dialod = new FolderBrowserDialog();
            var res = dialod.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(dialod.SelectedPath))
                doc.ReportInvoiceReport(Reports, dialod.SelectedPath);
        }
    }
}
