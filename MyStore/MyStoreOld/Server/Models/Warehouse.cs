using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{

    public class Warehouse : EntityBase
    {
        public Warehouse() : base() { }

        public Warehouse(EntityContext context) : base(context) { }


        private string name = "";
        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(nameof(Name)); }
        }


        private string description = "";
        public string Description
        {
            get { return description; }
            set { description = value; RaisePropertyChanged(nameof(Description)); }
        }

        private ObservableCollection<WarehouseProduct> products = new ObservableCollection<WarehouseProduct>();
        public ObservableCollection<WarehouseProduct> Products
        {
            get { return products; }
            set { products = value; RaisePropertyChanged(nameof(Products)); }
        }

        private ObservableCollection<Invoice> invoices = new ObservableCollection<Invoice>();
        public ObservableCollection<Invoice> Invoices
        {
            get { return invoices; }
            set { invoices = value; RaisePropertyChanged(nameof(Invoices)); }
        }

        private ObservableCollection<Supply> supplies = new ObservableCollection<Supply>();
        public ObservableCollection<Supply> Supplies
        {
            get { return supplies; }
            set { supplies = value; RaisePropertyChanged(nameof(Supplies)); }
        }

        public override string ToString()
        {
            return Name;
        }
    }

}
