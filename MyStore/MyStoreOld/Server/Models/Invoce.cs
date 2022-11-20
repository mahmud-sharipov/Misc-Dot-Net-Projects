using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{

    public class Invoice : EntityBase
    {
        public Invoice() : base() { }

        public Invoice(EntityContext context) : base(context) { }

        private DateTime date = DateTime.Now;
        public DateTime Date
        {
            get { return date; }
            set { date = value; RaisePropertyChanged(nameof(Date)); }
        }

        private double amount = 0;
        public double Amount
        {
            get { return amount; }
            set
            {
                if (Product != null && FromWarehouse != null && Product.Warehouse.Count > 0)
                {
                    var fromW = Product.Warehouse.Single(e => e.Warehouse.Id == FromWarehouse.Id);
                    if (value < 0) value = 0;
                    if (value > amount)
                    {
                        if (fromW.Amount < value - amount) value = amount + fromW.Amount;
                        Product.Amount -= value - amount;
                        fromW.Amount -= value - amount;
                    }
                    else
                    {
                        Product.Amount += amount - value;
                        fromW.Amount += amount - value;
                    }
                }
                amount = value;
                TotalPrice = Amount * Price;
                RaisePropertyChanged(nameof(Amount));
            }
        }

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

        private double totalPrice = 0;
        public double TotalPrice
        {
            get { return totalPrice; }
            set
            {
                totalPrice = value;
                RaisePropertyChanged(nameof(TotalPrice));
            }
        }

        private Product product;
        public Product Product
        {
            get { return product; }
            set
            {
                product = value;
                if (value != null) Price = product.Price;
                RaisePropertyChanged(nameof(Product));
            }
        }

        public MyDateTime MyDateTime
        {
            get { return new MyDateTime(Date); }
        }

        private Warehouse fromWarehouse;
        public Warehouse FromWarehouse
        {
            get { return fromWarehouse; }
            set
            {
                if (Product != null && value != null && Product.Warehouse.Count > 0)
                    Amount = 0;
                fromWarehouse = value; RaisePropertyChanged(nameof(FromWarehouse));
            }
        }

        private Debtor debtor;
        public Debtor Debtor
        {
            get { return debtor; }
            set { debtor = value; RaisePropertyChanged(nameof(Debtor)); }
        }
    }
}
