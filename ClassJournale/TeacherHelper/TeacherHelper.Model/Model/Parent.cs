namespace TeacherHelper.Model
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    public class Parent : User
    {
        public Parent() : base() { }

        public Parent(EntityContext context) : base(context) { }

        string _parentKind;
        public string ParentKind
        {
            get => _parentKind;
            set => _parentKind = OnPropertySetting(nameof(ParentKind), value, _parentKind);
        }

        private ICollection<Student> _children = new ObservableCollection<Student>();
        public virtual ICollection<Student> Children
        {
            get => _children;
            set => _children = OnPropertySetting(nameof(Children), value, _children);
        }

        public class Labels : User.Labels
        {
            public static string Label => "Student parent";
            public static string Children => "Children";
            public static string ParentKind => "Parent kind";

        }
    }

    public class ParentConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder, string prefix)
        {
            var config = modelBuilder.Entity<Parent>();
            config.ToTable(prefix + "Parents").HasKey(sp => sp.Guid);

            //reference
            config.HasMany(sp => sp.Children).WithMany(s => s.Parents).Map(sp =>
            {
                sp.MapLeftKey("ParentGuid");
                sp.MapRightKey("StudentGuid");
                sp.ToTable("StudentParents");
            });
        }
    }
}
