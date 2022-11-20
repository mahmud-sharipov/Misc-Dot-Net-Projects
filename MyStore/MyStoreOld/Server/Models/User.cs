using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Server.Models
{
    public class User : EntityBase
    {

        public User() : base() { }

        public User(EntityContext context) : base(context) { }

        private string name = "";
        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                RaisePropertyChanged(nameof(Name)); RaisePropertyChanged(nameof(FullName));
            }
        }

        private string firstName = "";
        public string FirstName
        {
            get { return this.firstName; }
            set
            {
                this.firstName = value;
                RaisePropertyChanged(nameof(FirstName)); RaisePropertyChanged(nameof(FullName));
            }
        }

        private string middleName = "";
        public string MiddleName
        {
            get { return this.middleName; }
            set
            {
                this.middleName = value;
                RaisePropertyChanged(nameof(MiddleName)); RaisePropertyChanged(nameof(FullName));
            }
        }

        public string FullName
        {
            get { return Name + " " + FirstName + " " + MiddleName; }
        }

        private string username = "";
        public string Username
        {
            get { return this.username; }
            set
            {
                this.username = value;
                RaisePropertyChanged(nameof(Username));
            }
        }

        private string password = "";
        public string Password
        {
            get { return this.password; }
            set
            {
                this.password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }
    }
}
