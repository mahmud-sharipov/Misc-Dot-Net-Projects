using Microsoft.Win32;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TexAnalyzer
{

    public partial class MainWindow
    {
        public static Window MyWindow;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new Parser();
            MyWindow = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName == "")
                return;
            (DataContext as Parser).FilePath = openFileDialog.FileName;
            var streamReader = new StreamReader(openFileDialog.FileName);
            //(DataContext as Parser).OriginText = streamReader.ReadToEnd();
            (DataContext as Parser).Text = streamReader.ReadToEnd().ToLower();
            streamReader.Dispose();
        }
    }
}
