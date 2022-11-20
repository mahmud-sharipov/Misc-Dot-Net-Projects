using Server.Dialogs;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Server.Contents
{
    public partial class DebtorPage : UserControl, INotifyPropertyChanged
    {
        private AppViewModel model_;
        public event PropertyChangedEventHandler PropertyChanged;

        public DebtorPage(AppViewModel model)
        {
            model_ = model;
            InitializeComponent();
            DataContext = this;
        }

        private string searchText = "";
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchText)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Debtors)));
            }
        }


        private List<Debtor> debtors;
        public List<Debtor> Debtors
        {
            get { return (searchText == "" ? model_.Debtors.ToList() : model_.Debtors.Where(e => e.Name.ToUpper().StartsWith(SearchText.ToUpper())).ToList()); }
        }


        private void Edit_click(object sender, RoutedEventArgs e)
        {
            if (DebtorList.SelectedItem is Debtor debtor)
            {
                var dialog = new AddEditDebtor(debtor);
                dialog.ShowDialog();
                if (dialog.Result != YesNoDialogResult.Yes)
                    dialog.Debtor.Context.Entry(dialog.Debtor).State = System.Data.Entity.EntityState.Unchanged;
                model_.DataProvider.SaveChanges();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DebtorList.SelectedItem is Debtor debtor)
            {
                var dialod = new YesNo("Вы действительно хотите удалить", "Удаление");
                dialod.ShowDialog();
                if (dialod.Result == YesNo.YesNoMessageBoxResult.Yes)
                {
                    model_.Debtors.Remove(debtor);
                    model_.DataProvider.Delete(debtor);
                    model_.DataProvider.SaveChanges();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Debtors)));
                }
            }
        }

        private void SearchBox_TextChanged(object sender, string e)
        {
            SearchText = e;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Debtors)));
        }
    }
}
