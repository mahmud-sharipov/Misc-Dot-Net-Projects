using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Server.Dialogs
{
    public partial class YesNo : Window, INotifyPropertyChanged
    {
        public YesNo(string message = "", string title = "")
        {
            InitializeComponent();
            Titles = title;
            Message = message;
            NoBtn.Focus();
            Result = YesNoMessageBoxResult.No;
            KeyUp += YesNo_KeyUp;
        }

        private void YesNo_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Close();
                    break;
            }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Message))); }
        }

        private string titles;
        public string Titles
        {
            get { return titles; }
            set { titles = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Titles))); }
        }

        private YesNoMessageBoxResult result;
        public YesNoMessageBoxResult Result
        {
            get { return result; }
            set { result = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result))); }
        }

        public enum YesNoMessageBoxResult
        {
            Yes,
            No
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void No_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void YesBtn_Click(object sender, RoutedEventArgs e)
        {
            Result = YesNoMessageBoxResult.Yes;
            this.Close();
        }
    }
}
