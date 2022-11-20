using MySql.Data.MySqlClient;
using Server.Contents;
using Server.Models;
using Server.UIElemetn;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Server
{
    public class AppViewModel : BaseViewModel
    {
        public AppViewModel() : this(new DataProvider()) { }
        public AppViewModel(DataProvider dp)
        {
            //WordDocument doc = new WordDocument("wordTemp/ProductAmountPrice.dot");

            DataProvider = dp;
            Products = new ObservableCollection<Product>(DataProvider.GetEntities<Product>().ToList());
            ProductTypes = new ObservableCollection<ProductType>(DataProvider.GetEntities<ProductType>().Where(e => e.Base == null).ToList());
            UOMs = new ObservableCollection<UOM>(DataProvider.GetEntities<UOM>().ToList());
            Invoices = new ObservableCollection<Invoice>(DataProvider.GetEntities<Invoice>().ToList());
            Supplies = new ObservableCollection<Supply>(DataProvider.GetEntities<Supply>().ToList());
            Suppliers = new ObservableCollection<Supplier>(DataProvider.GetEntities<Supplier>().ToList());
            Warehouses = new ObservableCollection<Warehouse>(DataProvider.GetEntities<Warehouse>().ToList());
            WarehouseProducts = new ObservableCollection<WarehouseProduct>(DataProvider.GetEntities<WarehouseProduct>().ToList());
            Debtors = new ObservableCollection<Debtor>(DataProvider.GetEntities<Debtor>().ToList());
            MenuItems = new ObservableCollection<ContentItem>();
            InitMenuItems();
            CurrentContent = 0;
        }

        #region properties
        //General
        private ObservableCollection<ContentItem> menuItems;
        public ObservableCollection<ContentItem> MenuItems
        {
            get { return menuItems; }
            set { menuItems = value; RaisePropertyChanged(nameof(MenuItems)); }
        }

        private int currentContent;
        public int CurrentContent
        {
            get { return currentContent; }
            set { currentContent = value; RaisePropertyChanged(nameof(CurrentContent)); }
        }


        private User currentUser;
        public User CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; RaisePropertyChanged(nameof(CurrentUser)); }
        }

        //ProductType
        private ObservableCollection<ProductType> productTypes;
        public ObservableCollection<ProductType> ProductTypes
        {
            get { return productTypes; }
            set { productTypes = value; RaisePropertyChanged(nameof(ProductTypes)); }
        }


        private ObservableCollection<UOM> uom;
        public ObservableCollection<UOM> UOMs
        {
            get { return uom; }
            set { uom = value; RaisePropertyChanged(nameof(UOMs)); }
        }

        //Products
        private ObservableCollection<Product> products;
        public ObservableCollection<Product> Products
        {
            get { return products; }
            set { products = value; RaisePropertyChanged(nameof(Products)); }
        }

        private LinkButton productInvoicesLink;
        public LinkButton ProductInvoicesLink
        {
            get
            {
                if (productInvoicesLink == null)
                    productInvoicesLink = new LinkButton()
                    {
                        Label = "Продажи",
                        LinkCommand = new RelayCommand(obj =>
                        {
                            new Contents.InvoiceReport(this, obj as Product).ShowDialog();
                        })
                    };
                return productInvoicesLink;
            }
        }

        private LinkButton productSuppliesLink;
        public LinkButton ProductSuppliesLink
        {
            get
            {
                if (productSuppliesLink == null)
                    productSuppliesLink = new LinkButton()
                    {
                        Label = "Поставки",
                        LinkCommand = new RelayCommand(obj =>
                        {
                            new Contents.SupplyReport(this, obj as Product).ShowDialog();
                        })
                    };
                return productSuppliesLink;
            }
        }

        //Invoices

        private ObservableCollection<Invoice> invoices;
        public ObservableCollection<Invoice> Invoices
        {
            get { return invoices; }
            set { invoices = value; RaisePropertyChanged(nameof(Invoices)); }
        }

        //Supply & Supplier

        private ObservableCollection<Supplier> suppliers;
        public ObservableCollection<Supplier> Suppliers
        {
            get { return suppliers; }
            set { suppliers = value; RaisePropertyChanged(nameof(Suppliers)); }
        }


        private ObservableCollection<Supply> supplies;
        public ObservableCollection<Supply> Supplies
        {
            get { return supplies; }
            set { supplies = value; RaisePropertyChanged(nameof(Supplies)); }
        }

        //Warehouse

        private ObservableCollection<Warehouse> warehouses;
        public ObservableCollection<Warehouse> Warehouses
        {
            get { return warehouses; }
            set { warehouses = value; RaisePropertyChanged(nameof(Warehouses)); }
        }


        private ObservableCollection<WarehouseProduct> warehouseProducts;
        public ObservableCollection<WarehouseProduct> WarehouseProducts
        {
            get { return warehouseProducts; }
            set { warehouseProducts = value; RaisePropertyChanged(nameof(WarehouseProducts)); }
        }

        //Debtor

        private ObservableCollection<Debtor> debtors = new ObservableCollection<Debtor>();
        public ObservableCollection<Debtor> Debtors
        {
            get { return debtors; }
            set { debtors = value; RaisePropertyChanged(nameof(Debtors)); }
        }

        #endregion

        #region Commands
        RelayCommand saveChanged;
        public RelayCommand SaveChange
        {
            get
            {
                if (saveChanged == null)
                    saveChanged = new RelayCommand(param =>
                    {
                        try
                        {
                            DataProvider.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    });
                return saveChanged;
            }
        }

        RelayCommand restart;
        public RelayCommand Restart
        {
            get
            {
                if (restart == null)
                    restart = new RelayCommand(param =>
                    {
                        var dump = CurrentContent;
                        switch (CurrentContent)
                        {
                            //case 0:
                            //    MenuItems[0] = new ContentItem("Асосӣ", new HomePage(new HomeViewModel(DataProvider, Successor)));
                            //    break;
                            //case 1:
                            //    MenuItems[1] = new ContentItem("Истифодабаранда", new UsersPage(new UsersViewModel(DataProvider)));
                            //    break;
                            //case 2:
                            //    MenuItems[2] = new ContentItem("Таҷҳизот", new DevicesPage(new DevicesViewModel(DataProvider)));
                            //    break;
                            //case 3:
                            //    MenuItems[3] = new ContentItem("Синфхона", new ClassroomsPage(new ClassroomViewModel(DataProvider)));
                            //    break;
                            //case 4:
                            //    MenuItems[4] = new ContentItem("Корт", new ChipsPage(new ChipViewModel(DataProvider,Successor)));
                            //    break;
                            //case 5:
                            //    MenuItems[5] = new ContentItem("Дафтари қайдҳо", new DeviceLogsPage(DataProvider));
                            //    break;
                        }
                        (param as MainWindow).ItemsListBox.SelectedIndex = dump;
                    });
                return restart;
            }
        }
     
        #endregion

        #region methods
        void InitMenuItems()
        {
            MenuItems.Add(new ContentItem("Главная", new HomePage(this)));
            MenuItems.Add(new ContentItem("Товары", new ProductsPage(this)));
            MenuItems.Add(new ContentItem("Вид товаров", new ProductTypePage(this)));
            MenuItems.Add(new ContentItem("Проданные товары", new InvoicesPege(this)));
            MenuItems.Add(new ContentItem("Поставки и поставщики", new SuppliersPage(this)));
            MenuItems.Add(new ContentItem("Склади", new WarehousePage(this)));
            MenuItems.Add(new ContentItem("Должники", new DebtorPage(this)));
            MenuItems.Add(new ContentItem("Другие", new OtherPage(this)));
        }

        public void SaveChanges()
        {
            DataProvider.SaveChanges();
        }
        #endregion
    }
}
