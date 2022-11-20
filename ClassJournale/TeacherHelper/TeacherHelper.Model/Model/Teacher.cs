namespace TeacherHelper.Model
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    public class Teacher : User
    {
        public Teacher() : base() { }

        public Teacher(EntityContext context) : base(context) { }

        string _login;
        public string Login
        {
            get => _login;
            set => _login = OnPropertySetting(nameof(Login), value, _login);
        }

        string _password;
        public string Password
        {
            get => _password;
            set => _password = OnPropertySetting(nameof(Password), value, _password);
        }

        public class Labels : User.Labels
        {
            public static string Label => "Teacher";
            public static string Login => "Login";
            public static string Password => "Password";

        }
    }

    public class TeacherConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder, string prefix)
        {
            var config = modelBuilder.Entity<Teacher>();
            config.ToTable(prefix + "Teachers").HasKey(t => t.Guid);


        }
    }
}
