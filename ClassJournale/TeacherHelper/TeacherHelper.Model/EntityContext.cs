﻿using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;

namespace TeacherHelper.Model
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class EntityContext : DbContext
    {
        public EntityContext() : base(GetConnectionString("TeacherHelper"))
        {
            if (!Database.Exists())
            {
                Database.Create();
                CreateDemoData(this);
            }

            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
            ObjectContext context = ((IObjectContextAdapter)this).ObjectContext;
            context.ObjectStateManager.ObjectStateManagerChanged += ObjectStateManager_ObjectStateManagerChanged;
            context.ObjectMaterialized += Context_ObjectMaterialized;
            context.Connection.StateChange += Connection_StateChange;
        }

        static string GetConnectionString(string dbName, string userName = "root", string pass = "") => string.Format(ConfigurationManager.ConnectionStrings["mysqlCon"].ConnectionString, dbName, userName, pass);

        void CreateDemoData(EntityContext context)
        {
            //var user = new User(context) { Username = "admin", Password = "admin", IsReadOnly = true };
            //var role = new Role(context) { Sku = "ADMIN", IsReadOnly = true, Name = "System Administrator" };
            //var userRole = new UserRole(user, role);
            //context.Add(user);
            //context.Add(role);
            //context.Add(userRole);
            //context.SaveChanges();
        }

        void Connection_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {

        }

        void Context_ObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            if (!(e.Entity is EntityBase entity))
                return;
            SetEntityContext(entity);
        }

        void ObjectStateManager_ObjectStateManagerChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e)
        {
            if (!(e.Element is EntityBase entity))
                return;
            SetEntityContext(entity);
        }

        void SetEntityContext(EntityBase entity)
        {
            if (entity.GetContext() == null)
                entity.SetContext(this);
        }

        #region GRUD

        public IQueryable<T> GetEntities<T>() where T : EntityBase => Set<T>().AsQueryable();

        public IQueryable<T> GetEntities<T>(IEnumerable<string> properties) where T : EntityBase
        {
            IQueryable<T> result = Set<T>();
            foreach (var property in properties)
            {
                result = result.Include(property);
            }
            return result;
        }

        public IQueryable GetEntities(Type typeOfEntity) => Set(typeOfEntity).AsQueryable();

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        protected ObjectContext GetObjectContext => ((IObjectContextAdapter)this).ObjectContext;

        public void Add(EntityBase entity, Type entityType) => Set(entityType).Add(entity);

        public void Add<T>(T entity) where T : EntityBase => Set<T>().Add(entity);

        public void Delete(EntityBase entity) => Set(entity.GetType()).Remove(entity);

        protected override bool ShouldValidateEntity(DbEntityEntry entityEntry) => true;

        #endregion

        #region Mapping
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
 => DbModelBuilderManager.BuildModels(modelBuilder);
        #endregion
    }
}
