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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Server.UIElemetn
{
    /// <summary>
    /// Логика взаимодействия для SearchBox.xaml
    /// </summary>
    public partial class SearchBox : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        #region TextChanged Event

        public event TextChangedEventHandler TextChanged;
        public virtual void OnTextChanged(string e)
        {
            TextChanged?.Invoke(this, e);
        }

        #region TextChanged EventArgs

        public delegate void TextChangedEventHandler(object sender, string e);

        #endregion

        #endregion

        public string SearchText
        {
            get { return (string)GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchText)));  }
        }
        public static readonly DependencyProperty SearchTextProperty =
            DependencyProperty.Register("SearchText", typeof(string), typeof(SearchBox));

        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }
        public static readonly DependencyProperty HintProperty =
            DependencyProperty.Register("Hint", typeof(string), typeof(SearchBox));

        public SearchBox()
        {
            InitializeComponent();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Field.Text = "";
        }

        private void Field_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Field.Text == string.Empty)
            {
                SearchBtn.Visibility = Visibility.Visible;
                CloseBtn.Visibility = Visibility.Hidden;
            }
            else
            {
                SearchBtn.Visibility = Visibility.Hidden;
                CloseBtn.Visibility = Visibility.Visible;
            }
            SearchText = Field.Text;
            OnTextChanged(Field.Text);
        }
    }
}
