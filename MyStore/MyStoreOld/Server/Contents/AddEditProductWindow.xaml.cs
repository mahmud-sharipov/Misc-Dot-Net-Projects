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
    public partial class AddEditProductWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Ctor
        public AddEditProductWindow() : this(new Product(), new DataProvider()) { }

        public AddEditProductWindow(DataProvider dp) : this(new Product(), dp) { }

        public AddEditProductWindow(Product product, DataProvider dp, bool isEdid = false)
        {
            Product = product;
            InitializeComponent();
            Result = YesNoMessageBoxResult.No;
            DataContext = product;
            if (isEdid)
            {
                UOMS = new ObservableCollection<UOM>(dp.GetEntities<UOM>().Where(e => e.Id != Product.Id).ToList());
                UOMS.Add(Product.UOM);

                Types = new ObservableCollection<ProductType>(dp.GetEntities<ProductType>().Where(e => e.Id != Product.Id).ToList());
                Types.Add(Product.Type);
                YesBtn.Content = "Сохранить";
            }
            else
            {
                UOMS = new ObservableCollection<UOM>(dp.GetEntities<UOM>().ToList());
                Types = new ObservableCollection<ProductType>(dp.GetEntities<ProductType>().ToList());
            }
        }
        #endregion

        private Product product;
        public Product Product
        {
            get { return product; }
            set { product = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Product))); }
        }


        private ObservableCollection<ProductType> types;
        public ObservableCollection<ProductType> Types
        {
            get { return (types != null ? new ObservableCollection<ProductType>(types.Where(e => e.StartWith(SearchType.Trim())).ToList()) : types); }
            set { types = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Types))); }
        }


        private ObservableCollection<UOM> uom;
        public ObservableCollection<UOM> UOMS
        {
            get { return uom; }
            set { uom = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UOMS))); }
        }


        private string searchType = "";
        public string SearchType
        {
            get { return searchType; }
            set
            {
                searchType = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchType)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Types)));
            }
        }


        private string errorText = "";
        public string ErrorText
        {
            get { return errorText = ""; }
            set { errorText = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorText))); }
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

        private void YesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Product.Type != null)
            {
                Result = YesNoMessageBoxResult.Yes;
                this.Close();
                return;
            }
            else
            {
                ErrorText = "Выберите вид товара";
            }
        }

        private void NoBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TypeComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            //SearchType = TypeComboBox.Text;
        }

        private void SearchBox_TextChanged(object sender, string e)
        {
            SearchType = e;
        }
    }
}
