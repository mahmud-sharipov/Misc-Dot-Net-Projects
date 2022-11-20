using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{

    public class WarehouseProduct : EntityBase
    {
        public WarehouseProduct() : base() { }

        public WarehouseProduct(EntityContext context) : base(context) { }


        private Product product;
        public Product Product
        {
            get { return product; }
            set { product = value; RaisePropertyChanged(nameof(Product)); }
        }

        private Warehouse warehouse;
        public Warehouse Warehouse
        {
            get { return warehouse; }
            set { warehouse = value; RaisePropertyChanged(nameof(Warehouse)); }
        }


        private double amount = 0;
        public double Amount
        {
            get { return amount; }
            set { amount = value; RaisePropertyChanged(nameof(Amount)); }
        }

    }

}
