using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ClassJoural.Models
{
    public class Student : EntityBase
    {
        public Student() : base() { }

        public Student(EntityContext context) : base(context) { }

        private string name;

        public string Name
        {
            get { return name == null ? "" : name; }
            set { name = value; RaisePropertyChanged(nameof(Name)); RaisePropertyChanged(nameof(FullName)); }
        }

        private string lastName;

        public string LastName
        {
            get { return lastName == null ? "" : lastName; }
            set { lastName = value; RaisePropertyChanged(nameof(LastName)); RaisePropertyChanged(nameof(FullName)); }
        }

        private string middleName;

        public string MiddleName
        {
            get { return middleName == null ? "" : middleName; }
            set { middleName = value; RaisePropertyChanged(nameof(MiddleName)); RaisePropertyChanged(nameof(FullName)); }
        }

        public string FullName
        {
            get { return string.Format("{0} {1}", LastName.Trim(), Name.Trim()); }
        }

        private DateTime dateOfBirth = DateTime.Now;
        public DateTime DateOfBirth
        {
            get
            {
                return dateOfBirth;
            }
            set { dateOfBirth = value; RaisePropertyChanged(nameof(DateOfBirth)); RaisePropertyChanged(nameof(DateOfBirthText)); }
        }
        string[] month = new string[] { "", "Янв", "Фев", "Мрт", "Апр", "Май ", "Июн", "Июл", "Авг", "Сен", "Окт", "Нбр", "Дек" };
        public string DateOfBirthText
        {
            get
            {
                return DateOfBirth.Day + " " + month[DateOfBirth.Month] + " " + DateOfBirth.Year;
            }
        }

        private string address = "";

        public string Address
        {
            get { return address; }
            set { address = value; RaisePropertyChanged(nameof(Address)); }
        }

        private bool isCurrent = true;

        public bool IsCurrent
        {
            get { return isCurrent; }
            set { isCurrent = value; RaisePropertyChanged(nameof(IsCurrent)); }
        }


        private ObservableCollection<ThemeDetail> estimations = new ObservableCollection<ThemeDetail>();

        public virtual ObservableCollection<ThemeDetail> Estimations
        {
            get { return estimations; }
            set { estimations = value; RaisePropertyChanged(nameof(Estimations)); }
        }

        private ObservableCollection<Parent> parents = new ObservableCollection<Parent>();

        public virtual ObservableCollection<Parent> Parents
        {
            get { return parents; }
            set { parents = value; RaisePropertyChanged(nameof(Parents)); }
        }

    }
}
