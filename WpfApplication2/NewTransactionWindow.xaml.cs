using System;
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

        public Account accountTextBoxText()
        { 
            return (Account)accountText.SelectedItem;
        }

        public double amountTextBoxText()
        { 
            double d = 0.00;
            if(double.TryParse(amountText.Text, out d))
                return double.Parse(amountText.Text);
            else
                return 0;
        }

        public string transacteeTextBoxText()
        {
            return transacteeText.Text;
        }

        public string tagTextBoxText()
        {
            return tagText.Text;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var VM = new CheckBookVM();
            DataContext = VM;
            VM.Fill();
        }



    }
}
