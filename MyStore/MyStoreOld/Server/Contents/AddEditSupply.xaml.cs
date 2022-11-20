using Server.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class AddEditSupply : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        bool isEdit_ = false;
        public AddEditSupply() : this(new Supply(), new AppViewModel()) { }

        public AddEditSupply(AppViewModel dp) : this(new Supply(), dp) { }

        public AddEditSupply(Supply sp, AppViewModel dp, bool isEdid = false)
        {
            Model = dp;
            Supply = sp;
            isEdit_ = isEdid;
            InitializeComponent();
            DataContext = Supply;
            Result = YesNoMessageBoxResult.No;
            Suppliers = new ObservableCollection<Supplier>(Model.DataProvider.GetEntities<Supplier>().ToList());
            if (isEdid)
                YesBtn.Content = "Сохранить";
        }

        public AppViewModel Model { get; set; }

        private Supply supply;
        public Supply Supply
        {
            get { return supply; }
            set { supply = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Supply))); }
        }


        private ObservableCollection<Supplier> suppliers;
        public ObservableCollection<Supplier> Suppliers
        {
            get
            {
                if (suppliers != null) return new ObservableCollection<Supplier>(suppliers.Where(e => e.Name.StartsWith(SearchText)).ToList());
                return suppliers;
            }
            set { suppliers = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Suppliers))); }
        }


        private YesNoMessageBoxResult result;
        public YesNoMessageBoxResult Result
        {
            get { return result; }
            set { result = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result))); }
        }
        public enum YesNoMessageBoxResult
        {
            Yes,
            No
        }


        private string errorText = "";
        public string ErrorText
        {
            get { return errorText = ""; }
            set { errorText = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorText))); }
        }

        private string searchText = "";
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchText)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Supply)));
            }
        }


        private void YesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Supply.ToWarehouse == null)
            {
                MessageBox.Show("Выберите склад");
                return;
            }
            Result = YesNoMessageBoxResult.Yes;
            Close();
        }
        private void NoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!isEdit_) Supply.Amount = 0;
            Close();
        }

        private void SearchBox_TextChanged(object sender, string e)
        {
            SearchText = e;
        }
    }
}
