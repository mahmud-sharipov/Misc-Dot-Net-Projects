using Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Server.Contents
{
    public partial class AddEditSupplier : Window,INotifyPropertyChanged
    {
        private DataProvider dp_;
        public Supplier user_;
        public event PropertyChangedEventHandler PropertyChanged;

        public AddEditSupplier() : this(new Supplier()) { }

        public AddEditSupplier(Supplier user, bool isEdit = false)
        {
            user_ = user;
            InitializeComponent();
            DataContext = user_;
            Result = YesNoDialogResult.No;
        }

        private YesNoDialogResult result;
        public YesNoDialogResult Result
        {
            get { return result; }
            set { result = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result))); }
        }

        private void YesBtn_Click(object sender, RoutedEventArgs e)
        {
            Result = YesNoDialogResult.Yes;
            Close();
        }

        private void NoBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
