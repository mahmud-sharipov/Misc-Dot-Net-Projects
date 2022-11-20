using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Product : EntityBase
    {
        public Product() : base() { }

        public Product(EntityContext context) : base(context) { }

        private string name = "";
        public string Name
        {
            get { return this.name; }
            set { this.name = value; RaisePropertyChanged(nameof(Name)); }
        }


        private double amount = 0;
        public double Amount
        {
            get { return amount; }
            set { amount = value; RaisePropertyChanged(nameof(Amount)); }
        }


        private double price = 0;
        public double Price
        {
            get { return price; }
            set { price = value; RaisePropertyChanged(nameof(Price)); }
        }


        private int number = 1;
        public int Number
        {
            get { return number; }
            set { number = value; RaisePropertyChanged(nameof(Number)); }
        }


        private string code = "";
        public string Code
        {
            get { return code; }
            set { code = value; RaisePropertyChanged(nameof(Code)); }
        }

        public virtual double TotalQuantity => Warehouse.Sum(w => w.Amount);

        private UOM uom;
        public virtual UOM UOM
        {
            get { return uom; }
            set { uom = value; RaisePropertyChanged(nameof(UOM)); }
        }


        private ProductType type;
        public virtual ProductType Type
        {
            get { return type; }
            set { type = value; RaisePropertyChanged(nameof(Type)); }
        }


        private ObservableCollection<Supply> supplies = new ObservableCollection<Supply>();
        public virtual ObservableCollection<Supply> Supplies
        {
            get { return supplies; }
            set { supplies = value; RaisePropertyChanged(nameof(Supplies)); }
        }


        private ObservableCollection<Invoice> invoices = new ObservableCollection<Invoice>();
        public ObservableCollection<Invoice> Invoices
        {
            get { return invoices; }
            set { invoices = value; RaisePropertyChanged(nameof(Invoices)); }
        }

        private ObservableCollection<WarehouseProduct> warehouse = new ObservableCollection<WarehouseProduct>();
        public ObservableCollection<WarehouseProduct> Warehouse
        {
            get { return warehouse; }
            set { warehouse = value; RaisePropertyChanged(nameof(Warehouse)); }
        }

    }
}
