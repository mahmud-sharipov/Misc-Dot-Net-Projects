using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherHelper.Model
{
    public class DataProvider
    {
        public DataProvider() => Context = Global.Context;

        EntityContext Context { get; set; }

        public IQueryable<TEntity> GetEntities<TEntity>() where TEntity : EntityBase =>
            Context.GetEntities<TEntity>();

        public TEntity Add<TEntity>() where TEntity : EntityBase, new()
        {
            var entity = new TEntity();
            Context.Add(entity);
            addedEntities.Add(entity);
            return entity;
        }

        List<EntityBase> addedEntities = new List<EntityBase>();

        public void Delete(EntityBase entity) => Context.Delete(entity);

        public void SaveChanges()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error on saving context", ex);
            }
        }
    }
}
