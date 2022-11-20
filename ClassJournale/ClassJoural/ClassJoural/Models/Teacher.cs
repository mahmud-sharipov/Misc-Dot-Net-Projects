using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ClassJoural.Models
{
    public class Teacher : EntityBase
    {
        public Teacher() : base() { }

        public Teacher(EntityContext context) : base(context) { }

        private string fio = "";

        public string FIO
        {
            get { return fio; }
            set { fio = value; RaisePropertyChanged(nameof(FIO)); }
        }

        private ObservableCollection<Subject> subjects=new ObservableCollection<Subject>();

        public virtual ObservableCollection<Subject> Subjects
        {
            get { return subjects; }
            set { subjects = value; RaisePropertyChanged(nameof(Subjects)); }
        }

        private Setting setting;

        public Setting Setting
        {
            get { return setting; }
            set { setting = value; RaisePropertyChanged(nameof(Setting)); }
        }


    }
}
