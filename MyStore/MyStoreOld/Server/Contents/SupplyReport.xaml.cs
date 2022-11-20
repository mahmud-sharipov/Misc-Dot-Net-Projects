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
    public partial class SupplyReport : Window, INotifyPropertyChanged
    {
        private AppViewModel model_;
        public event PropertyChangedEventHandler PropertyChanged;
        ObservableCollection<UIElemetn.SupplyReport> reports = new ObservableCollection<UIElemetn.SupplyReport>();
        bool isOne = false;

        public SupplyReport(AppViewModel model)
        {
            model_ = model;
            foreach (var item in model_.Products)
                reports.Add(new UIElemetn.SupplyReport() { Product = item, Supplier = CurrentSupplier, Warehouse = CurrentWarehouse });
            InitializeComponent();
            FilterContent.DataContext = model_;
            DataContext = this;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Reports)));
        }

        public SupplyReport(AppViewModel model, Product product)
        {
            model_ = model;
            isOne = true;
            reports.Add(new UIElemetn.SupplyReport() { Product = product, Supplier = CurrentSupplier, Warehouse = CurrentWarehouse });
            InitializeComponent();
            FilterContent.DataContext = model_;
            DataContext = this;
        }

        public SupplyReport(AppViewModel model, Warehouse warehouse) : this(model) { CurrentWarehouse = warehouse; }

        public SupplyReport(AppViewModel model, Supplier supplier) : this(model) { CurrentSupplier = supplier; }

        public List<UIElemetn.SupplyReport> Reports
        {
            get { return (VisibilEmptyItem ? reports.ToList() : reports.Where(e => e.Amount != 0).ToList()); }
        }

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

        private Supplier currentSupplier;
        public Supplier CurrentSupplier
        {
            get { return currentSupplier; }
            set
            {
                currentSupplier = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentSupplier)));
                foreach (var item in reports)
                    item.Supplier = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Reports)));
            }
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SupplierList.SelectedIndex = -1;
            WarehouseList.SelectedIndex = -1;
            if (isOne)
            {
                reports.Clear();
                foreach (var item in model_.Products)
                    reports.Add(new UIElemetn.SupplyReport() { Product = item, Supplier = CurrentSupplier, Warehouse = CurrentWarehouse });
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
                doc.ReportSypplyReport(Reports, dialod.SelectedPath);
        }
    }
}
