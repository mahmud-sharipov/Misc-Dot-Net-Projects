using Server.Models;
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
using System.Windows.Shapes;

namespace Server.Contents
{
    public partial class SwapProduct : Window, INotifyPropertyChanged
    {
        private AppViewModel model_;
        public event PropertyChangedEventHandler PropertyChanged;

        public SwapProduct(AppViewModel model, WarehouseProduct product)
        {
            model_ = model;
            Product = product;
            InitializeComponent();
            DataContext = model_;
        }

        public WarehouseProduct WarehouseProduct { get; set; }

        private WarehouseProduct product;
        public WarehouseProduct Product
        {
            get { return product; }
            set { product = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Product))); }
        }

        private Warehouse to;
        public Warehouse To
        {
            get { return to; }
            set
            {
                to = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(To)));
                ToWarehousePeoduct = Product.Product.Warehouse.Single(e => e.Warehouse.Id == value.Id);
            }
        }

        private double amount;
        public double Amount
        {
            get { return amount; }
            set
            {
                if (value == 0)
                    amount = 0;
                else if (value > Product.Amount)
                {
                    amount = Product.Amount;
                }
                else
                    amount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Amount)));
            }
        }

        public WarehouseProduct ToWarehousePeoduct { get; set; }

        private YesNoDialogResult result = YesNoDialogResult.No;
        public YesNoDialogResult Result
        {
            get { return result; }
            set { result = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result))); }
        }

        private void YesBtn_Click(object sender, RoutedEventArgs e)
        {
            Result = YesNoDialogResult.Yes;
            Close();
        }

        private void NoBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
