using Server.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.UIElemetn
{
    public class SupplyReport : INotifyPropertyChanged
    {
        private Product product;
        public Product Product
        {
            get { return product; }
            set { product = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Product))); Refresh(); }
        }

        private Warehouse warehouse;
        public Warehouse Warehouse
        {
            get { return warehouse; }
            set { warehouse = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Warehouse))); Refresh(); }
        }

        private Supplier supplier;
        public Supplier Supplier
        {
            get { return supplier; }
            set { supplier = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Supplier))); Refresh(); }
        }

        public double Amount
        {
            get
            {
                if (Product != null)
                {
                    var amount = Product.Supplies.Where(e =>
                                            (Warehouse == null ? true : e.ToWarehouse.Id == Warehouse.Id) &&
                                            (Supplier == null ? true : e.Supplier.Id == Supplier.Id)
                                        ).Sum(e => e.Amount);
                    return amount;
                }
                return 0;
            }

        }

        public double TotalPrice
        {
            get
            {
                if (Product != null)
                {
                    var sum = Product.Supplies.Where(e =>
                                            (Warehouse == null ? true : e.ToWarehouse.Id == Warehouse.Id) &&
                                            (Supplier == null ? true : e.Supplier.Id == Supplier.Id)
                                        ).Sum(e => e.TotalPrice);
                    return sum;
                }
                return 0;
            }
        }

        public void Refresh()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Amount)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalPrice)));
        }
        public event PropertyChangedEventHandler PropertyChanged;

    }

    public class InvoiceReport : INotifyPropertyChanged
    {

        private Product product;
        public Product Product
        {
            get { return product; }
            set { product = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Product))); Refresh(); }
        }

        private Warehouse warehouse;
        public Warehouse Warehouse
        {
            get { return warehouse; }
            set { warehouse = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Warehouse))); Refresh(); }
        }

        private DateTime date = DateTime.Now;
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Date)));
                if (EqualDate) Refresh();
            }
        }


        private bool equalDate;
        public bool EqualDate
        {
            get { return equalDate; }
            set { equalDate = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EqualDate))); Refresh(); }
        }


        public double Amount
        {
            get
            {
                if (Product != null)
                {
                    var amount = Product.Invoices.Where(e =>
                                            (Warehouse == null ? true : e.FromWarehouse.Id == Warehouse.Id) &&
                                            (EqualDate ? e.Date.Date == Date.Date : true)
                                        ).Sum(e => e.Amount);
                    return amount;
                }
                return 0;
            }

        }

        public double TotalPrice
        {
            get
            {
                if (Product != null)
                {
                    var sum = Product.Invoices.Where(e =>
                                            (Warehouse == null ? true : e.FromWarehouse.Id == Warehouse.Id) &&
                                            (EqualDate ? e.Date.Date == Date.Date : true)
                                        ).Sum(e => e.TotalPrice);
                    return sum;
                }
                return 0;
            }
        }

        public void Refresh()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Amount)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalPrice)));
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
