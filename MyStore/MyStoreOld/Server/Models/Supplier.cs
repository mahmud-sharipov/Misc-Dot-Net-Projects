using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{

    public class Supplier : EntityBase
    {
        public Supplier() : base() { }

        public Supplier(EntityContext context) : base(context) { }


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


        private ObservableCollection<Supply> supplies = new ObservableCollection<Supply>();
        public virtual ObservableCollection<Supply> Supplies
        {
            get { return supplies; }
            set { supplies = value; RaisePropertyChanged(nameof(Supplies)); }
        }
    }

}
