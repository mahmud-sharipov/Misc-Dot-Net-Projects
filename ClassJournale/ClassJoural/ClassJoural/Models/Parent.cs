using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ClassJoural.Models
{
    public class Parent : EntityBase
    {
        public Parent() : base() { }

        public Parent(EntityContext context) : base(context) { }

        private string fio = "";

        public string FIO
        {
            get { return fio; }
            set { fio = value; RaisePropertyChanged(nameof(FIO)); }
        }

        private string phone = "";

        public string Phone
        {
            get { return phone; }
            set { phone = value; RaisePropertyChanged(nameof(Phone)); }
        }

        private ObservableCollection<Student> children = new ObservableCollection<Student>();

        public virtual ObservableCollection<Student> Children
        {
            get { return children; }
            set { children = value; RaisePropertyChanged(nameof(Children)); }
        }



    }
}
