using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ContentItem : INotifyPropertyChanged
    {
        private string _name;
        private object _content;

        public ContentItem(string name, object content)
        {
            _name = name;
            _content = content;
        }


        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(nameof(Name)); }
        }

        public object Content
        {
            get { return _content; }
            set { _content = value; RaisePropertyChanged(nameof(Content)); }
        }


        #region INotifyPropertyChangrd
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
