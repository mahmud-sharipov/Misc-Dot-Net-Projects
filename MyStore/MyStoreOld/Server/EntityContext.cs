using MySql.Data.Entity;
using Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Server
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class EntityContext : DbContext
    {
        public EntityContext() : base(GetConnectionString("MyStore1"))
        {
            try
            {
                if (!Database.Exists())
                {
                    Database.Create();
                    CreateBlankData();
                }
                Database.CreateIfNotExists();
                Configuration.LazyLoadingEnabled = true;
                Configuration.ProxyCreationEnabled = true;
                ObjectContext context = ((IObjectContextAdapter)this).ObjectContext;
                context.ObjectStateManager.ObjectStateManagerChanged += ObjectStateManager_ObjectStateManagerChanged;
                context.ObjectMaterialized += Context_ObjectMaterialized;
                context.Connection.StateChange += Connection_StateChange;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateBlankData()
        {
            var entity = new UOM() { Name = "м", Description = "Метр" };
            entity.SetContext(this);
            Entry(entity).State = System.Data.Entity.EntityState.Added;

            var entity1 = new UOM() { Name = "см", Description = "Сантиметр" };
            entity.SetContext(this);
            Entry(entity).State = System.Data.Entity.EntityState.Added;

            var entity2 = new UOM() { Name = "м2", Description = "Квадратный метр" };
            entity2.SetContext(this);
            Entry(entity2).State = System.Data.Entity.EntityState.Added;

            var entity3 = new UOM() { Name = "см2", Description = "Квадратный сантиметр" };
            entity3.SetContext(this);
            Entry(entity3).State = System.Data.Entity.EntityState.Added;

            var entity4 = new UOM() { Name = "л", Description = "Литр" };
            entity4.SetContext(this);
            Entry(entity4).State = System.Data.Entity.EntityState.Added;

            var entity5 = new UOM() { Name = "м3", Description = "Кубическый метр" };
            entity5.SetContext(this);
            Entry(entity5).State = System.Data.Entity.EntityState.Added;

            var entity6 = new UOM() { Name = "т", Description = "Тонна" };
            entity6.SetContext(this);
            Entry(entity6).State = System.Data.Entity.EntityState.Added;

            var entity7 = new UOM() { Name = "кг", Description = "Клограмм" };
            entity7.SetContext(this);
            Entry(entity7).State = System.Data.Entity.EntityState.Added;

            var user = new User() { Username = "admin", Password = ("admin").GetHashCode().ToString() };
            user.SetContext(this);
            Entry(user).State = System.Data.Entity.EntityState.Added;

            SaveChanges();

        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        protected override bool ShouldValidateEntity(DbEntityEntry entityEntry)
        {
            return true;
        }

        void Connection_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {
        }

        void Context_ObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            var entity = e.Entity as EntityBase;
            if (entity == null)
                return;
            SetEntityContext(entity);
        }

        void ObjectStateManager_ObjectStateManagerChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e)
        {
            var entity = e.Element as EntityBase;
            if (entity == null)
                return;
            SetEntityContext(entity);
        }

        void SetEntityContext(EntityBase entity)
        {
            if (entity.Context == null)
            {
                entity.SetContext(this);
            }
        }

        static string GetConnectionString(string dbName, string userName = "root", string pass = "")
        {
            if (ConfigurationManager.ConnectionStrings != null)
            {
                var connString = ConfigurationManager.ConnectionStrings["mysqlCon"].ConnectionString;
                return String.Format(connString, dbName, userName, pass);
            }
            return "";
        }

        public void AddToContext<T>(T entity) where T : EntityBase
        {
            Entry(entity).State = EntityState.Added;
        }

        public void Delete<T>(T entity) where T : EntityBase
        {
            Entry(entity).State = EntityState.Deleted;
        }

        public IQueryable<T> GetEntities<T>() where T : EntityBase
        {
            try
            {
                return Set<T>();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid Entity Type supplied for Lookup", ex);
            }
        }

        public IQueryable<T> GetEntities<T>(IEnumerable<string> properties) where T : EntityBase
        {
            IQueryable<T> result = Set<T>();

            foreach (var property in properties)
            {
                result = result.Include(property);
            }
            return result;

        }

        public IQueryable GetEntities(Type typeOfEntity)
        {
            return Set(typeOfEntity).AsQueryable();
        }

        #region Mapping
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Ignore<EntityBase>();
            modelBuilder.Entity<EntityBase>().Ignore(p => p.Context);
            modelBuilder.Entity<EntityBase>().Ignore(p => p.IsSelected);
            modelBuilder.Entity<EntityBase>().HasKey(e => e.Id);

            UserMapping(modelBuilder);
            ProductMapping(modelBuilder);
            ProductTypeMapping(modelBuilder);
            InvoiceMapping(modelBuilder);
            SupplierMapping(modelBuilder);
            SupplyMapping(modelBuilder);
            UOMMapping(modelBuilder);
            WarehouseMapping(modelBuilder);
            WarehouseProductMapping(modelBuilder);
        }
        void UserMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            //
        }
        void ProductMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Products");
            //
            modelBuilder.Entity<Product>().HasRequired(e => e.Type).WithMany(e => e.Products).WillCascadeOnDelete(true);
            modelBuilder.Entity<Product>().HasOptional(e => e.UOM).WithMany(e => e.Products).WillCascadeOnDelete(true);
            modelBuilder.Entity<Product>().HasMany(e => e.Invoices).WithRequired(e => e.Product).WillCascadeOnDelete(true);
            modelBuilder.Entity<Product>().HasMany(e => e.Supplies).WithRequired(e => e.Product).WillCascadeOnDelete(true);

        }
        void ProductTypeMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductType>().ToTable("ProductTypes");
            modelBuilder.Entity<ProductType>().Ignore(e => e.FullName);
            //
            modelBuilder.Entity<ProductType>().HasMany(u => u.Products).WithRequired(c => c.Type).WillCascadeOnDelete(true);
            modelBuilder.Entity<ProductType>().HasOptional(u => u.Base).WithMany(c => c.Childs).WillCascadeOnDelete(true);
        }
        void InvoiceMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>().ToTable("Invoices");
            modelBuilder.Entity<Invoice>().Ignore(e => e.MyDateTime);
            //
            modelBuilder.Entity<Invoice>().HasRequired(dl => dl.Product).WithMany(d => d.Invoices).WillCascadeOnDelete(true);
            modelBuilder.Entity<Invoice>().HasRequired(dl => dl.FromWarehouse).WithMany(d => d.Invoices).WillCascadeOnDelete(true);
            modelBuilder.Entity<Invoice>().HasOptional(d => d.Debtor).WithRequired(c => c.Invoice).WillCascadeOnDelete(true);
        }
        void SupplierMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Supplier>().ToTable("Suppliers");
            //
            modelBuilder.Entity<Supplier>().HasMany(d => d.Supplies).WithOptional(c => c.Supplier).WillCascadeOnDelete(true);
        }
        void SupplyMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Supply>().ToTable("Supplies");
            modelBuilder.Entity<Supply>().Ignore(e => e.TotalPrice);
            //
            modelBuilder.Entity<Supply>().HasRequired(d => d.Product).WithMany(c => c.Supplies).WillCascadeOnDelete(true);
            modelBuilder.Entity<Supply>().HasOptional(d => d.Supplier).WithMany(c => c.Supplies).WillCascadeOnDelete(true);
            modelBuilder.Entity<Supply>().HasRequired(d => d.ToWarehouse).WithMany(c => c.Supplies).WillCascadeOnDelete(true);
        }
        void UOMMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UOM>().ToTable("UOMS");
            //
            modelBuilder.Entity<UOM>().HasMany(d => d.Products).WithOptional(c => c.UOM).WillCascadeOnDelete();
        }
        void WarehouseMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Warehouse>().ToTable("Warehouses");
            //
            modelBuilder.Entity<Warehouse>().HasMany(d => d.Products).WithRequired(c => c.Warehouse).WillCascadeOnDelete(true);
            modelBuilder.Entity<Warehouse>().HasMany(d => d.Supplies).WithRequired(c => c.ToWarehouse).WillCascadeOnDelete(true);
            modelBuilder.Entity<Warehouse>().HasMany(d => d.Invoices).WithRequired(c => c.FromWarehouse).WillCascadeOnDelete(true);
        }
        void WarehouseProductMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WarehouseProduct>().ToTable("WarehouseProducts");
            //
            modelBuilder.Entity<WarehouseProduct>().HasRequired(d => d.Product).WithMany(c => c.Warehouse).WillCascadeOnDelete(true);
            modelBuilder.Entity<WarehouseProduct>().HasRequired(d => d.Warehouse).WithMany(c => c.Products).WillCascadeOnDelete(true);
        }

        void DebtorMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Debtor>().ToTable("Debtors");
            //
            modelBuilder.Entity<Debtor>().HasRequired(d => d.Invoice).WithOptional(c => c.Debtor).WillCascadeOnDelete(true);
        }
        #endregion

    }
}
