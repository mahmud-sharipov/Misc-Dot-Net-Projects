using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{

    public class UOM : EntityBase
    {
        public UOM() : base() { }

        public UOM(EntityContext context) : base(context) { }

        private string name = "";
        public string Name
        {
            get { return this.name; }
            set { this.name = value; RaisePropertyChanged(nameof(Name)); }
        }


        private string description = "";
        public string Description
        {
            get { return description; }
            set { description = value; RaisePropertyChanged(nameof(Description)); }
        }


        private ObservableCollection<Product> products = new ObservableCollection<Product>();
        public virtual ObservableCollection<Product> Products
        {
            get { return products; }
            set { products = value; RaisePropertyChanged(nameof(Products)); }
        }

        public override string ToString()
        {
            return Description;
        }
    }

}
