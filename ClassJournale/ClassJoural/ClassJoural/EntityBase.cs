using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ClassJoural
{
    public class EntityBase : INotifyPropertyChanged
    {
        protected EntityBase() { }

        protected EntityBase(EntityContext context)
        {
            Context = context;
            AddToContext(context);
        }

        private Guid guid;

        public Guid Id
        {
            get
            {
                if (guid == Guid.Empty)
                {
                    guid = Guid.NewGuid();
                }
                return guid;
            }
            set
            {
                guid = value;
            }
        }

        public EntityContext Context { get; private set; }

        public void SetContext(EntityContext context)
        {
            Context = context;
        }

        public void DeleteEntity()
        {
            Context.Delete(this);
        }

        void AddToContext(EntityContext context)
        {
            if (context != null)
                context.Entry(this).State = System.Data.Entity.EntityState.Added;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
