using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{

    public class Debtor : EntityBase
    {
        public Debtor() : base() { }

        public Debtor(EntityContext context) : base(context) { }


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


        private double remains;
        public double Remains
        {
            get { return remains; }
            set { remains = value; RaisePropertyChanged(nameof(Remains)); }
        }

        private Invoice invoice;
        public Invoice Invoice
        {
            get { return invoice; }
            set { invoice = value; RaisePropertyChanged(nameof(Invoice)); }
        }

    }

}
