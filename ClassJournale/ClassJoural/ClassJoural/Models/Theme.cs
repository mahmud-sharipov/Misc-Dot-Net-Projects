using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ClassJoural.Models
{
    public class Theme : EntityBase
    {
        public Theme() : base() { }

        public Theme(EntityContext context) : base(context) { }

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
            set { description = value;RaisePropertyChanged(nameof(Description)); }
        }

        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; RaisePropertyChanged(nameof(Date)); RaisePropertyChanged(nameof(DateText)); }
        }

        public string DateText { get { return string.Format("{0}.{1}.{2}", Date.Day, Date.Month, Date.Year); } }

        private bool isExam;

        public bool IsExam
        {
            get { return isExam; }
            set { isExam = value;RaisePropertyChanged(nameof(IsExam)); }
        }

        private bool isClose;

        public bool IsClose
        {
            get { return isClose; }
            set { isClose = value; RaisePropertyChanged(nameof(IsClose)); }
        }

        private Subject subject;

        public Subject Subject
        {
            get { return subject; }
            set { subject = value;RaisePropertyChanged(nameof(Subject)); }
        }

        private ObservableCollection<ThemeDetail> details=new ObservableCollection<ThemeDetail>();

        public virtual ObservableCollection<ThemeDetail> Details
        {
            get { return details; }
            set { details = value; RaisePropertyChanged(nameof(Details)); }
        }


    }
}
