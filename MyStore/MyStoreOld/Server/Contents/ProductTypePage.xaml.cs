using Server.Dialogs;
using Server.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Server.Contents
{
    public partial class ProductTypePage : UserControl, INotifyPropertyChanged
    {
        AppViewModel model_;
        public event PropertyChangedEventHandler PropertyChanged;

        public ProductTypePage(AppViewModel model)
        {
            model_ = model;
            InitializeComponent();
            DataContext = model_;
            Types.Add(new TreeItems() { Label = "Все" });
            LoadTree(model_.ProductTypes, Types[0].Items, Types[0]);
        }

        private ObservableCollection<TreeItems> types = new ObservableCollection<TreeItems>();
        public ObservableCollection<TreeItems> Types
        {
            get { return types; }
            set { types = value; }
        }

        public List<Product> Products
        {
            get
            {
                var seIt = TreeTypes.SelectedItem as TreeItems;
                if (seIt == null) return null;
                if (seIt.Type == null) return model_.Products.ToList();
                return seIt.Type.GetProducts();
            }
        }


        void LoadTree(ObservableCollection<ProductType> InList, ObservableCollection<TreeItems> toList, TreeItems base_ = null)
        {
            foreach (var item in InList)
            {
                var nI = new TreeItems() { Type = item, Label = item.Name, Base = base_ };
                if (item.Childs != null) LoadTree(item.Childs, nI.Items, nI);
                toList.Add(nI);
            }
        }

        private void DeleteType_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (TreeTypes.SelectedItem == null) return;
            var dialog = new YesNo("Вы действительно хотите удалить", "Удаление");
            dialog.ShowDialog();
            if (dialog.Result == YesNo.YesNoMessageBoxResult.Yes)
            {
                var selectedItem = TreeTypes.SelectedItem as TreeItems;
                if (selectedItem.Base != null)
                {
                    if (selectedItem.Type.Base == null)
                        model_.ProductTypes.Remove(selectedItem.Type);
                    else
                        selectedItem.Type.Base.Childs.Remove(selectedItem.Type);

                    if (selectedItem.Type.Childs != null && selectedItem.Type.Childs.Count > 0) DeleteChaild(selectedItem.Type);
                    selectedItem.Base.Items.Remove(selectedItem);
                    model_.DataProvider.Delete(selectedItem.Type);
                    model_.DataProvider.SaveChanges();
                }
            }
        }

        void DeleteChaild(ProductType type)
        {
            try
            {
                foreach (var item in type.Childs)
                {
                    if (item.Childs != null && item.Childs.Count > 0) DeleteChaild(item);
                    model_.DataProvider.Delete(item);
                }
            }
            catch { }
        }

        private void EditTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (TreeTypes.SelectedItem is TreeItems treeItem && treeItem.Type != null)
            {
                var dialog = new AddEditProductType(treeItem.Type);
                dialog.ShowDialog();
                if (dialog.Result == YesNoDialogResult.Yes)
                    model_.DataProvider.SaveChanges();
                else
                    dialog.Type.Context.Entry(dialog.Type).State = System.Data.Entity.EntityState.Unchanged;
            }
        }

        private void AddTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddEditProductType();
            dialog.ShowDialog();
            if (dialog.Result == YesNoDialogResult.Yes)
            {
                var base_ = TreeTypes.SelectedItem as TreeItems;
                if (base_ != null && base_.Type != null)
                {
                    dialog.Type.Base = base_.Type;
                    base_.Type.Childs.Add(dialog.Type);
                    base_.Items.Add(new TreeItems() { Type = dialog.Type, Label = dialog.Type.Name, Base = base_ });
                }
                else
                {
                    var nIt = new TreeItems() { Type = dialog.Type, Label = dialog.Type.Name, Base = Types[0] };
                    Types[0].Items.Add(nIt);
                    model_.ProductTypes.Add(dialog.Type);
                }
                model_.DataProvider.Add(dialog.Type);
                model_.DataProvider.SaveChanges();
            }
        }

        private void TreeTypes_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.Delete:
                    DeleteType_Click(null, null);
                    break;
                case System.Windows.Input.Key.Enter:
                    AddTypeBtn_Click(null, null);
                    break;
                case System.Windows.Input.Key.F1:
                    EditTypeBtn_Click(null, null);
                    break;
            }
        }

        private void TreeTypes_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Products)));
        }
    }

    public class TreeItems : INotifyPropertyChanged
    {
        private void Type_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Name") Label = Type.Name;
        }


        private TreeItems base_;
        public TreeItems Base
        {
            get { return base_; }
            set { base_ = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs((nameof(Base)))); }
        }


        private ProductType type;
        public ProductType Type
        {
            get { return type; }
            set
            {
                type = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Type)));
                if (value != null) value.PropertyChanged += Type_PropertyChanged;
            }
        }


        private string label;
        public string Label
        {
            get { return label; }
            set { label = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Label))); }
        }

        private ObservableCollection<TreeItems> items = new ObservableCollection<TreeItems>();
        public ObservableCollection<TreeItems> Items
        {
            get { return items; }
            set { items = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Items))); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
