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
    public partial class AddEditProductType : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public AddEditProductType() : this(new ProductType()) { }
        public AddEditProductType(ProductType type)
        {
            Type = type;
            InitializeComponent();
            DataContext = Type;
            Result = YesNoDialogResult.No;
        }


        private ProductType type;
        public ProductType Type
        {
            get { return type; }
            set { type = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Type))); }
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
