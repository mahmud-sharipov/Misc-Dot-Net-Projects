using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassJoural.Models
{
    public class ThemeDetail : EntityBase
    {
        public ThemeDetail() : base() { }

        public ThemeDetail(EntityContext context) : base(context) { }

        private int estimation;

        public int Estimation
        {
            get { return estimation; }
            set
            {
                if (value < 0) { estimation = 0; RaisePropertyChanged(nameof(Estimation)); }
                else if (value > 5) { estimation = 5; RaisePropertyChanged(nameof(Estimation)); }
                else { estimation = value; RaisePropertyChanged(nameof(Estimation)); }
            }
        }

        private bool isAttend;

        public bool IsAttend
        {
            get { return isAttend; }
            set { isAttend = value; RaisePropertyChanged(nameof(IsAttend)); if (value == true) { Estimation = 0; RaisePropertyChanged(nameof(Estimation)); } }
        }

        private Student student;

        public virtual Student Student
        {
            get { return student; }
            set { student = value; RaisePropertyChanged(nameof(Student)); }
        }

        private Theme theme;

        public virtual Theme Theme
        {
            get { return theme; }
            set { theme = value; RaisePropertyChanged(nameof(Theme)); }
        }


    }
}
