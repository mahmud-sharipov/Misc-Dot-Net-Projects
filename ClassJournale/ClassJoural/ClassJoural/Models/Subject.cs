using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ClassJoural.Models
{
    public class Subject : EntityBase
    {
        public Subject() : base() { }

        public Subject(EntityContext context) : base(context) { }

        private string name = "";

        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(nameof(Name)); }
        }

        private int subjectHourCount;

        public int SubjectHourCount
        {
            get { return subjectHourCount; }
            set { subjectHourCount = value; RaisePropertyChanged(nameof(SubjectHourCount)); }
        }

        private Teacher teacher;

        public virtual Teacher Teacher
        {
            get { return teacher; }
            set { teacher = value; RaisePropertyChanged(nameof(Teacher)); }
        }

        private Schedule schedule;

        public virtual Schedule Schedule
        {
            get { return schedule; }
            set { schedule = value; RaisePropertyChanged(nameof(Schedule)); }
        }

        private ObservableCollection<Theme> themes = new ObservableCollection<Theme>();

        public virtual ObservableCollection<Theme> Themes
        {
            get { return themes; }
            set { themes = value; RaisePropertyChanged(nameof(Themes)); }
        }

    }
}
