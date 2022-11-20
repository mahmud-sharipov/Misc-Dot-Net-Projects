namespace TeacherHelper.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    public class JournalSettings : EntityBase
    {
        public JournalSettings() : base() { }

        public JournalSettings(EntityContext context) : base(context) { }

        int _yearStudy;
        public int YearStudy
        {
            get => _yearStudy;
            set => _yearStudy = OnPropertySetting(nameof(YearStudy), value, _yearStudy);
        }

        string _group;
        public string Group
        {
            get => _group;
            set => _group = OnPropertySetting(nameof(Group), value, _group);
        }

        int _classSubjectCount;
        public int ClassSubjectCount
        {
            get => _classSubjectCount;
            set => _classSubjectCount = OnPropertySetting(nameof(ClassSubjectCount), value, _classSubjectCount);
        }

        int _classStudentCount;
        public int ClassStudentCount
        {
            get => _classStudentCount;
            set => _classStudentCount = OnPropertySetting(nameof(ClassStudentCount), value, _classStudentCount);
        }

        Teacher _formMaster;
        public virtual Teacher FormMaster
        {
            get => _formMaster;
            set => _formMaster = OnPropertySetting(nameof(FormMaster), value, _formMaster);
        }

        DateTime _quarter1Start;
        public DateTime Quarter1Start
        {
            get => _quarter1Start;
            set => _quarter1Start = OnPropertySetting(nameof(Quarter1Start), value, _quarter1Start);
        }

        DateTime _quarter2Start;
        public DateTime Quarter2Start
        {
            get => _quarter2Start;
            set => _quarter2Start = OnPropertySetting(nameof(Quarter2Start), value, _quarter2Start);
        }

        DateTime _quarter3Start;
        public DateTime Quarter3Start
        {
            get => _quarter3Start;
            set => _quarter3Start = OnPropertySetting(nameof(Quarter3Start), value, _quarter3Start);
        }

        DateTime _quarter4Start;
        public DateTime Quarter4Start
        {
            get => _quarter4Start;
            set => _quarter4Start = OnPropertySetting(nameof(Quarter4Start), value, _quarter4Start);
        }

        DateTime _quarter1End;
        public DateTime Quarter1End
        {
            get => _quarter1End;
            set => _quarter1End = OnPropertySetting(nameof(Quarter1End), value, _quarter1End);
        }

        DateTime _quarter2End;
        public DateTime Quarter2End
        {
            get => _quarter2End;
            set => _quarter2End = OnPropertySetting(nameof(Quarter2End), value, _quarter2End);
        }

        DateTime _quarter3End;
        public DateTime Quarter3End
        {
            get => _quarter3End;
            set => _quarter3End = OnPropertySetting(nameof(Quarter3End), value, _quarter3End);
        }

        DateTime _quarter4End;
        public DateTime Quarter4End
        {
            get => _quarter4End;
            set => _quarter4End = OnPropertySetting(nameof(Quarter4End), value, _quarter4End);
        }

        public class Labels : EntityBase.Labels
        {
            public static string Label => "Journal settings";
            public static string YearStudy => "YearStudy";
            public static string Group => "Group";
            public static string ClassSubjectCount => "ClassSubjectCount";
            public static string ClassStudentCount => "ClassStudentCount";
            public static string FormMaster => "FormMaster";
            public static string Quarter1Start => "Quarter1 Start";
            public static string Quarter2Start => "Quarter2 Start";
            public static string Quarter3Start => "Quarter3 Start";
            public static string Quarter4Start => "Quarter4 Start";
            public static string Quarter1End => "Quarter1 End";
            public static string Quarter2End => "Quarter2 End";
            public static string Quarter3End => "Quarter3 End";
            public static string Quarter4End => "Quarter4 End";

        }
    }

    public class JournalSettingsConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder, string prefix)
        {
            var config = modelBuilder.Entity<JournalSettings>();
            config.ToTable(prefix + "JournalSettings").HasKey(s => s.Guid);

            //reference
            config.HasOptional(s => s.FormMaster).WithMany().WillCascadeOnDelete(false);
        }
    }
}
