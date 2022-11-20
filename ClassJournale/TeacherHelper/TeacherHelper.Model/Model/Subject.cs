namespace TeacherHelper.Model
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    public class Subject : EntityBase
    {
        public Subject(EntityContext context) : base(context) { }

        string _name;
        public string Name
        {
            get => _name;
            set => _name = OnPropertySetting(nameof(Name), value, _name);
        }

        string _description;
        public string Description
        {
            get => _description;
            set => _description = OnPropertySetting(nameof(Description), value, _description);
        }

        public class Labels : EntityBase.Labels
        {
            public static string Label => "Subject";
            public static string Name => "Name";
            public static string Description => "Description";

        }
    }

    public class SubjectConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder, string prefix)
        {
            var config = modelBuilder.Entity<Subject>();
            config.ToTable(prefix + "Subjects").HasKey(s => s.Guid);

        }
    }
}
