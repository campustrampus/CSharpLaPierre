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

namespace CSharpLaPierre
{
    public partial class NewTransactionWindow : Window
    {
        public NewTransactionWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var VM = new TransactionVM();
            DataContext = VM;
        }

        private void Close_Window(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}