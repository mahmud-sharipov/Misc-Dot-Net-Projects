using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class BaseViewModel: INotifyPropertyChanged
    {
        protected DataProvider dataProvider;

        public DataProvider DataProvider
        {
            get { return dataProvider; }
            protected set { dataProvider = value; RaisePropertyChanged(nameof(DataProvider)); }
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
