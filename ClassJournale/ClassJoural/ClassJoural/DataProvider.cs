using ClassJoural.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ClassJoural
{
    public class DataProvider
    {
       public EntityContext EFContext { get; set; }

        List<EntityBase> addedEntities = new List<EntityBase>();

        public Setting Setting { get; set; }


        public DataProvider()
        {
            EFContext = new EntityContext();
           // x:
            if (!EFContext.IsDbExists)
            {
                var s = Add<Setting>();
                s.YearStudy = 1;
                s.ClassStudentCount = 15;
                s.ClassSubjectCount = 12;
                s.ClassTeacher = Add<Teacher>();
                s.ClassTeacher.FIO = "Ному насаби муаллим";
                SaveChanges();
                var dialog = new FirstRaning(this);
                dialog.ShowDialog();
                //if (dialog.setting.FirsrRun == true)
                 //   goto x;
            }
            else if (EFContext.GetEntities<Setting>().First().FirsrRun == true)
            {
                var dialog = new FirstRaning(this);
                dialog.ShowDialog();
              //  if (dialog.setting.FirsrRun == true)
                 //   goto x;
            }
        }

        public IQueryable<TEntity> GetEntities<TEntity>() where TEntity : EntityBase
        {
            return EFContext.GetEntities<TEntity>();
        }

        public TEntity Add<TEntity>() where TEntity : EntityBase, new()
        {
            var entity = new TEntity();
            entity.SetContext(EFContext);
            EFContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
            addedEntities.Add(entity);
            return entity;
        }

        public void Add(EntityBase entity)
        {
            entity.SetContext(EFContext);
            EFContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
            addedEntities.Add(entity);
        }

        public void Delete(EntityBase entity)
        {
            if (entity != null)
            {
                var v = EFContext.Entry(entity).State;
                if (EFContext.Entry(entity).State == System.Data.Entity.EntityState.Added)
                {
                    EFContext.Entry(entity).State = System.Data.Entity.EntityState.Unchanged;
                    return;
                }
                EFContext.Delete(entity);
                EFContext.SaveChanges();
            }
        }

        public void SaveChanges()
        {
            EFContext.SaveChanges();
        }

        public void Close()
        {
            EFContext.Dispose();
        }

        public void DeleteDb()
        {
            EFContext.Database.Delete();
        }

    }
}
