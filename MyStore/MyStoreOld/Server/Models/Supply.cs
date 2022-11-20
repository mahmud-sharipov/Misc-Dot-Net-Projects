using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{

    public class Supply : EntityBase
    {
        public Supply() : base() { }

        public Supply(EntityContext context) : base(context) { }


        private double price = 0;
        public double Price
        {
            get { return price; }
            set
            {
                price = value;
                TotalPrice = Amount * Price;
                RaisePropertyChanged(nameof(Price));
            }
        }


        private double amount = 0;
        public double Amount
        {
            get { return amount; }
            set
            {
                if (Product != null && ToWarehouse != null && Product.Warehouse.Count > 0)
                {
                    if (ToWarehouse == null) return;
                    var toW = Product.Warehouse.Single(e => e.Warehouse.Id == ToWarehouse.Id);
                    if (value < 0) value = 0;
                    if (value > amount)
                    {
                        Product.Amount += value - amount;
                        toW.Amount += value - amount;
                    }
                    else
                    {
                        if (toW.Amount < amount - value) value = amount - toW.Amount;
                        Product.Amount -= amount - value;
                        toW.Amount -= amount - value;
                    }
                }
                amount = value;
                TotalPrice = Amount * Price;
                RaisePropertyChanged(nameof(Amount));
            }
        }


        private double totalPrice = 0;
        public double TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; RaisePropertyChanged(nameof(TotalPrice)); }
        }


        private DateTime date = DateTime.Now;
        public DateTime Date
        {
            get { return date; }
            set { date = value; RaisePropertyChanged(nameof(Date)); }
        }


        private Product product;
        public virtual Product Product
        {
            get { return product; }
            set { product = value; RaisePropertyChanged(nameof(Product)); }
        }


        private Supplier supplier;
        public virtual Supplier Supplier
        {
            get { return supplier; }
            set { supplier = value; RaisePropertyChanged(nameof(Supplier)); }
        }


        private Warehouse toWarehouse;
        public Warehouse ToWarehouse
        {
            get { return toWarehouse; }
            set
            {
                if (Product != null && value != null && Product.Warehouse.Count > 0)
                {
                    if (ToWarehouse != null)
                    {
                        var toW = Product.Warehouse.Single(e => e.Warehouse.Id == ToWarehouse.Id);
                        toW.Amount -= Amount;
                        toW.Product.Amount -= Amount;
                    }
                    var NtoW = Product.Warehouse.Single(e => e.Warehouse.Id == value.Id);
                    NtoW.Amount += Amount;
                    NtoW.Product.Amount += Amount;
                }
                toWarehouse = value;
                RaisePropertyChanged(nameof(ToWarehouse));
                Amount = Amount;
            }
        }

    }

}
