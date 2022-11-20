using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{

    public class ProductType : EntityBase
    {
        public ProductType() : base() { }

        public ProductType(EntityContext context) : base(context) { }


        private string name = "";
        public string Name
        {
            get { return this.name; }
            set { this.name = value; RaisePropertyChanged(nameof(Name)); RaisePropertyChanged(nameof(FullName)); }
        }


        public virtual string FullName
        {
            get { return (Base != null ? Base.FullName + "->" : "") + Name; }
        }


        private string description = "";
        public string Description
        {
            get { return this.description; }
            set { this.description = value; RaisePropertyChanged(nameof(Description)); RaisePropertyChanged(nameof(FullName)); }
        }


        private ProductType base_;
        public virtual ProductType Base
        {
            get { return base_; }
            set { base_ = value; RaisePropertyChanged(nameof(Base)); }
        }


        private ObservableCollection<ProductType> childs = new ObservableCollection<ProductType>();
        public virtual ObservableCollection<ProductType> Childs
        {
            get { return childs; }
            set { childs = value; RaisePropertyChanged(nameof(Childs)); }
        }


        private ObservableCollection<Product> products = new ObservableCollection<Product>();
        public virtual ObservableCollection<Product> Products
        {
            get { return products; }
            set { products = value; RaisePropertyChanged(nameof(Products)); }
        }

        public override string ToString()
        {
            return FullName;
        }

        public bool StartWith(string text, string childName = "")
        {
            return (Name + "-" + childName).ToUpper().StartsWith(text.ToUpper()) || (Base != null ? Base.StartWith(text, Name + "-" + childName) : false);
        }

        public List<Product> GetProducts()
        {
            var products = new List<Product>();
            products.AddRange(Products.ToList());
            foreach (var item in Childs)
            {
                products.AddRange(item.GetProducts());
            }
            return products;
        }
    }

}
