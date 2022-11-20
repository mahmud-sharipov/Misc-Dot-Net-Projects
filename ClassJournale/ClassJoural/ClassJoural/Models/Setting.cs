using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassJoural.Models
{
    public class Setting : EntityBase
    {
        public Setting() : base() { }

        public Setting(EntityContext context) : base(context) { }

        private int yearStudy;

        public int YearStudy
        {
            get { return yearStudy; }
            set { yearStudy = value; RaisePropertyChanged(nameof(YearStudy)); }
        }

        private string group = "";

        public string Group
        {
            get { return group; }
            set { group = value; RaisePropertyChanged(nameof(Group)); }
        }

        private int classSubjectCount;

        public int ClassSubjectCount
        {
            get { return classSubjectCount; }
            set { classSubjectCount = value; RaisePropertyChanged(nameof(ClassSubjectCount)); }
        }

        private int classStudentCount;

        public int ClassStudentCount
        {
            get { return classStudentCount; }
            set { classStudentCount = value; RaisePropertyChanged(nameof(ClassStudentCount)); }
        }

        private bool firstRun = true;

        public bool FirsrRun
        {
            get { return firstRun; }
            set { firstRun = value; RaisePropertyChanged(nameof(FirsrRun)); }
        }



        private Teacher classTeacher;

        public Teacher ClassTeacher
        {
            get { return classTeacher; }
            set { classTeacher = value; RaisePropertyChanged(nameof(ClassTeacher)); }
        }
    }
}
