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
    public partial class AddEditWarehouse : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public AddEditWarehouse() : this(new Warehouse()) { }
        public AddEditWarehouse(Warehouse uom)
        {
            Warehouse = uom;
            InitializeComponent();
            DataContext = Warehouse;
            Result = YesNoDialogResult.No;
        }

        private Warehouse warehouse;
        public Warehouse Warehouse
        {
            get { return warehouse; }
            set { warehouse = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Warehouse))); }
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
