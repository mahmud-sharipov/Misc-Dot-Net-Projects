using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class DataProvider
    {
        public EntityContext EFContext { get; set; }

        List<EntityBase> addedEntities = new List<EntityBase>();

        public DataProvider()
        {
            EFContext = new EntityContext();
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
                if (EFContext.Entry(entity).State == EntityState.Added)
                {
                    EFContext.Entry(entity).State = EntityState.Detached;
                    return;
                }
                EFContext.Delete(entity);
            }
        }

        public void SaveChanges()
        {
            EFContext.SaveChanges();
        }

        public void RollBack()
        {
            var changedEntries = EFContext.ChangeTracker.Entries()
                .Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }
        public void Close()
        {
            EFContext.Dispose();
        }

    }
}
