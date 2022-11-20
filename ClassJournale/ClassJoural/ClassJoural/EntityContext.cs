using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Data.Entity.ModelConfiguration.Conventions;
using ClassJoural.Models;
using System.IO;

namespace ClassJoural
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class EntityContext : DbContext
    {
        public bool IsDbExists = true;
        public EntityContext() : base("Server=localhost;Database=ClassJournal;UserId=root;CharSet=utf8; Persist Security Info=False")
        {
            IsDbExists = Database.Exists();
            Database.CreateIfNotExists();
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
            ObjectContext context = ((IObjectContextAdapter)this).ObjectContext;
            context.ObjectStateManager.ObjectStateManagerChanged += ObjectStateManager_ObjectStateManagerChanged;
            context.ObjectMaterialized += Context_ObjectMaterialized;
            context.Connection.StateChange += Connection_StateChange;
        }

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
            modelBuilder.Entity<EntityBase>().HasKey(e => e.Id);
            StudentMapping(modelBuilder);
            ParentMapping(modelBuilder);
            ThemeMapping(modelBuilder);
            ThemeDetailMapping(modelBuilder);
            SubjectMapping(modelBuilder);
            TeacherMapping(modelBuilder);
            ScheduleMapping(modelBuilder);
            SettingMapping(modelBuilder);
        }
        void StudentMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Students");
            //

            modelBuilder.Entity<Student>().HasMany(e => e.Parents).WithMany(e => e.Children).
                Map(ec =>
                {
                    ec.MapLeftKey("StudentId");
                    ec.MapRightKey("ParentId");
                    ec.ToTable("StudentParent");
                }
                );
            modelBuilder.Entity<Student>().HasMany(e => e.Estimations).WithRequired(e => e.Student).WillCascadeOnDelete();
        }
        void ParentMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parent>().ToTable("Parents");

            //
            modelBuilder.Entity<Parent>().HasMany(e => e.Children).WithMany(e => e.Parents).
               Map(ec =>
               {
                   ec.MapLeftKey("ParentId");
                   ec.MapRightKey("StudentId");
                   ec.ToTable("StudentParent");
               }
               );
        }
        void ThemeMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Theme>().ToTable("Themes");
            //
            modelBuilder.Entity<Theme>().HasMany(e => e.Details).WithRequired(e => e.Theme).WillCascadeOnDelete();
            modelBuilder.Entity<Theme>().HasRequired(e => e.Subject).WithMany(e => e.Themes).WillCascadeOnDelete();
        }
        void ThemeDetailMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ThemeDetail>().ToTable("ThemeDetails");
            //
            modelBuilder.Entity<ThemeDetail>().HasRequired(e => e.Theme).WithMany(e => e.Details).WillCascadeOnDelete();
            modelBuilder.Entity<ThemeDetail>().HasRequired(e => e.Student).WithMany(e => e.Estimations).WillCascadeOnDelete();

        }
        void SubjectMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>().ToTable("Subjects");
            //
            modelBuilder.Entity<Subject>().HasMany(e => e.Themes).WithRequired(e => e.Subject).WillCascadeOnDelete();
            modelBuilder.Entity<Subject>().HasOptional(e => e.Teacher).WithMany(e => e.Subjects).WillCascadeOnDelete();
            modelBuilder.Entity<Subject>().HasOptional(e => e.Schedule).WithRequired(e => e.Subject).WillCascadeOnDelete();
        }
        void TeacherMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>().ToTable("Teachers");
            //
            modelBuilder.Entity<Teacher>().HasOptional(e => e.Setting).WithRequired(e => e.ClassTeacher).WillCascadeOnDelete();
        }
        void ScheduleMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schedule>().ToTable("Schedules");
            //
            modelBuilder.Entity<Schedule>().HasRequired(e => e.Subject).WithOptional(e => e.Schedule).WillCascadeOnDelete();
        }
        void SettingMapping(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Setting>().ToTable("Setting");
            //
            modelBuilder.Entity<Setting>().HasRequired(e => e.ClassTeacher).WithOptional(e => e.Setting).WillCascadeOnDelete();
        }
        #endregion
    }
}
