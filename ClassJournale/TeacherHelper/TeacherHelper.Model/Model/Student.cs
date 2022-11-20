namespace TeacherHelper.Model
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    public class Student : User
    {
        public Student() : base() { }

        public Student(EntityContext context) : base(context) { }

        private ICollection<Parent> _parents = new ObservableCollection<Parent>();
        public virtual ICollection<Parent> Parents
        {
            get => _parents;
            set => _parents = OnPropertySetting(nameof(Parents), value, _parents);
        }

        public class Labels : User.Labels
        {
            public static string Label => "Student";
            public static string Parents => "Parents";

        }
    }

    public class StudentConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder, string prefix)
        {
            var config = modelBuilder.Entity<Student>();
            config.ToTable(prefix + "Students").HasKey(s => s.Guid);

            //reference
            config.HasMany(sp => sp.Parents).WithMany(s => s.Children).Map(sp =>
            {
                sp.MapLeftKey("StudentGuid");
                sp.MapRightKey("ParentGuid");
                sp.ToTable("StudentParents");
            });
        }
    }
}
