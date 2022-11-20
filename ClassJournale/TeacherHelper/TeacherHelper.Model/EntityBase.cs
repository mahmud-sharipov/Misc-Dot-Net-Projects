﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Reflection;

namespace TeacherHelper.Model
{
    public class EntityBase : INotifyPropertyChanged
    {
        public EntityBase() { }

        public EntityBase(EntityContext context)
        {
            _context = context;
            _context.Add(this, GetType());
        }

        private Guid guid;
        public Guid Guid
        {
            get
            {
                if (guid == Guid.Empty) guid = Guid.NewGuid();
                return guid;
            }
            protected set
            {
                if (guid == Guid.Empty)
                    guid = value;
            }
        }

        EntityContext _context;

        public class Labels
        {
            public static string Label => "Entity base";
            public static string Guid => "Guid";
            public static string IsReadOnly => "Is ReadOnly";
            public static string Context => "Context";
        }

        protected TProperty OnPropertySetting<TProperty>(string propertyName, TProperty newValue, TProperty oldValue)
        {
            //var _newValue = oldValue;
            //BLManager.PropertySetter(this, propertyName, newValue, oldValue, (TProperty value) => { _newValue = value; RaisePropertyChanged(propertyName); });
            return newValue;
        }

        public void SetContext(EntityContext context) => _context = context;

        public EntityContext GetContext() => _context;

        public void Delete() => _context.Delete(this);

        void AddContext(EntityContext context)
        {
            if (context == null)
                context.Entry(this).State = EntityState.Added;
        }

        #region Indexer
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

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }

    public class EntityBaseConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder, string prefix)
        {
            modelBuilder.Ignore<EntityBase>();
            var config = modelBuilder.Entity<EntityBase>();
        }
    }
}
