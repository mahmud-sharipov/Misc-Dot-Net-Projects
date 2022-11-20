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
    public partial class AddEditUOM : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public AddEditUOM() : this(new UOM()) { }
        public AddEditUOM(UOM uom)
        {
            UOM = uom;
            InitializeComponent();
            DataContext = UOM;
            Result = YesNoDialogResult.No;
        }

        private UOM uom;
        public UOM UOM
        {
            get { return uom; }
            set { uom = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UOM))); }
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
