
using System;
namespace TeacherHelper.Model
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    public class User : EntityBase
    {
        public User() : base() { }

        public User(EntityContext context) : base(context) { }

        #region General

        string _lastName;
        public string LastName
        {
            get => _lastName;
            set => _lastName = OnPropertySetting(nameof(LastName), value, _lastName);
        }

        string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => _firstName = OnPropertySetting(nameof(FirstName), value, _firstName);
        }

        string _patronymic;
        public string Patronymic
        {
            get => _patronymic;
            set => _patronymic = OnPropertySetting(nameof(Patronymic), value, _patronymic);
        }

        string _address;
        public string Address
        {
            get => _address;
            set => _address = OnPropertySetting(nameof(Address), value, _address);
        }

        DateTime _birthday;
        public DateTime Birthday
        {
            get => _birthday;
            set => _birthday = OnPropertySetting(nameof(Birthday), value, _birthday);
        }

        string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = OnPropertySetting(nameof(PhoneNumber), value, _phoneNumber);
        }

        string _email;
        public string Email
        {
            get => _email;
            set => _email = OnPropertySetting(nameof(Email), value, _email);
        }

        bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set => _isActive = OnPropertySetting(nameof(IsActive), value, _isActive);
        }

        string _description;
        public string Description
        {
            get => _description;
            set => _description = OnPropertySetting(nameof(Description), value, _description);
        }

        public virtual string FullName => $"{LastName} {FirstName} {Patronymic}";

        public virtual string Title => $"{Labels.FullName}: {FullName} \t\n {Labels.Birthday}: {Birthday} \t\n {Labels.Address}: {Address}";
        #endregion

        public class Labels : EntityBase.Labels
        {
            public static string Label => "User";
            public static string LastName => "Last name";
            public static string FirstName => "First name";
            public static string Patronymic => "Patronymic";
            public static string Address => "Address";
            public static string Birthday => "Birthday";
            public static string PhoneNumber => "PhoneNumber";
            public static string Email => "Email";
            public static string IsActive => "IsActive";
            public static string Description => "Description";
            public static string FullName => "FullName";
            public static string Title => "Description";

        }
    }

    public class UserConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder, string prefix)
        {
            var config = modelBuilder.Entity<User>();
            config.ToTable(prefix + "Users").HasKey(u => u.Guid);
            config.Ignore(u => u.Title);
            config.Ignore(u => u.FullName);
        }
    }
}