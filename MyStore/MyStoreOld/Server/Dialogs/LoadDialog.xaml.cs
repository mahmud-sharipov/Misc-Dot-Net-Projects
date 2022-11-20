﻿using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Логика взаимодействия для LoadDialog.xaml
    /// </summary>
    public partial class LoadDialog : Window
    {
        public LoadDialog(string title)
        {
            InitializeComponent();
            TitleTb.Text = title;
        }
    }
}
