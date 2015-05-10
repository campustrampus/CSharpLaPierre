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


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var VM = new TransactionVM();
            DataContext = VM;
        }



    }

    public class TransactionVM: BaseVM
    {
         

        private Transaction _Data = new Transaction { Date = DateTime.Now } ;
        public Transaction Data
        {
            get { return _Data; }
            set { _Data = value; }
        }

        private IEnumerable<Account> _Accounts;
        public IEnumerable<Account> Accounts
        {
            get { return _Db.Accounts.ToList(); }
            //set { _Accounts = value; }
        }
        
        
        
    }
}
