using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class MyDateTime : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MyDateTime()
        {

        }
        public MyDateTime(DateTime date)
        {

        }

        public string Date
        {
            get
            {
                return string.Format("{0}/{1}/{2}", dateTime.Day, dateTime.Month, dateTime.Year);
            }
        }
        public string Time
        {
            get
            {
                return string.Format("{0}/{1}/{2}", dateTime.Hour, dateTime.Minute, dateTime.Second);
            }
        }

        DateTime dateTime;
        public DateTime DateTime
        {
            get { return dateTime; }
            set
            {
                dateTime = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DateTime)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Date)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Time)));
            }
        }

    }
}
