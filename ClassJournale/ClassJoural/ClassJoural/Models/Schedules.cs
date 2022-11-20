using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassJoural.Models
{
    public class Schedule : EntityBase
    {
        public Schedule() : base() { }

        public Schedule(EntityContext context) : base(context) { }

        private Subject subject;
        public virtual Subject Subject
        {
            get { return subject; }
            set { subject = value; RaisePropertyChanged(nameof(Subject)); }
        }


        private bool dushanbe = false;
        public bool Dushanbe
        {
            get { return dushanbe; }
            set { dushanbe = value; RaisePropertyChanged(nameof(Dushanbe)); }
        }


        private bool seshanbe = false;
        public bool Seshanbe
        {
            get { return seshanbe; }
            set { seshanbe = value; RaisePropertyChanged(nameof(Seshanbe)); }
        }


        private bool chorshanbe = false;
        public bool Chorshanbe
        {
            get { return chorshanbe; }
            set { chorshanbe = value; RaisePropertyChanged(nameof(Chorshanbe)); }
        }


        private bool panjshanbe = false;
        public bool Panjshanbe
        {
            get { return panjshanbe; }
            set { panjshanbe = value; RaisePropertyChanged(nameof(Panjshanbe)); }
        }


        private bool juma = false;
        public bool Juma
        {
            get { return juma; }
            set { juma = value; RaisePropertyChanged(nameof(Juma)); }
        }


        private bool shanbe = false;
        public bool Shanbe
        {
            get { return shanbe; }
            set { shanbe = value; RaisePropertyChanged(nameof(Shanbe)); }
        }

        public bool GetDayValue(string day)
        {
            switch (day)
            {
                case "Monday": return Dushanbe;
                case "Tuesday": return Seshanbe;
                case "Wednesday": return Chorshanbe;
                case "Thursday": return Panjshanbe;
                case "Friday": return Juma;
                case "Saturday": return Shanbe;
                default:
                    break;
            }
            return false;
        }
    }
}
