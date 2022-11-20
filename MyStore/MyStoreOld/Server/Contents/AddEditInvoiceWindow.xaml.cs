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
    public partial class AddEditInvoiceWindow : Window, INotifyPropertyChanged
    {
        bool isEdit_ = false;
        public event PropertyChangedEventHandler PropertyChanged;

        public AddEditInvoiceWindow(AppViewModel model) : this(new Invoice(), model) { }
        public AddEditInvoiceWindow(Invoice invoice, AppViewModel model, bool isEdit = false)
        {
            Model = model;
            isEdit_ = isEdit;
            Invoice = invoice;
            InitializeComponent();
            DataContext = Invoice;
            Result = YesNoMessageBoxResult.No;
        }


        private Invoice invoice;
        public Invoice Invoice
        {
            get { return invoice; }
            set { invoice = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Invoice))); }
        }

        public AppViewModel Model { get; set; }

        private YesNoMessageBoxResult result;
        public YesNoMessageBoxResult Result
        {
            get { return result; }
            set { result = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result))); }
        }

        public enum YesNoMessageBoxResult
        {
            Yes,
            No
        }

        private void YesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Invoice.FromWarehouse == null)
            {
                MessageBox.Show("Выберите склад");
                return;
            }
            Result = YesNoMessageBoxResult.Yes;
            Close();
        }

        private void NoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!isEdit_) Invoice.Amount = 0;
            Close();
        }
    }
}
