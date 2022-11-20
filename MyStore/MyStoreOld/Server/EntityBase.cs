using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Server
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

        #region Indexer

        /// <summary>
        /// Gets or sets the value for a specified property
        /// </summary>
        /// <param name="propertyName">Property name</param>
        /// <returns></returns>
        public object this[string propertyName]
        {
            get
            {
                object obj = null;
                if (propertyName != null && propertyName.Contains("."))
                {
                    int index = propertyName.IndexOf(".");
                    string transferablePropertyName = propertyName.Substring(0, index);

                    EntityBase transferable = this[transferablePropertyName] as EntityBase;
                    if (transferable == null)
                        return null;

                    obj = transferable[propertyName.Substring(index + 1)];
                    return obj;

                }
                Type type = GetType();
                if (type.GetTypeInfo().IsAssignableFrom(typeof(IList<>).GetTypeInfo()))
                {
                    if (propertyName == "Count")
                    {
                        obj = type.GetProperty(propertyName).GetValue(this);
                    }
                }
                else
                {
                    PropertyInfo propertyInfo = type.GetProperty(propertyName);
                    if (propertyInfo == null)
                    {
                        throw new ArgumentException("The '" + propertyName + "' property does not exist in " + type.Name);
                    }
                    obj = propertyInfo.GetValue(this);
                }
                return obj;
            }
            set
            {
                Type type = GetType();
                PropertyInfo propertyInfo = type.GetProperty(propertyName);
                if (propertyInfo == null)
                {
                    throw new ArgumentException("The '" + propertyName + "' property does not exist in " + type.Name);
                }
                if (propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(this, value);
                }
                else
                {
                    throw new InvalidOperationException("Attempting to set a readonly property.  Type:" + type.Name + " Property:" + propertyName);
                }
            }
        }
        #endregion

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

        private bool isSelected;

        public virtual bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; RaisePropertyChanged(nameof(IsSelected)); }
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
