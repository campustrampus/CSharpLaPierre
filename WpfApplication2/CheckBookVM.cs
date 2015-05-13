
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

namespace CSharpLaPierre
{

    public class CheckBookVM : BaseVM
    {
        public CheckBookVM()
        {
        }

        CbDb _Db = new CbDb();

        public Transaction _NewTransaction = new Transaction { Date = DateTime.Now };
        public Transaction NewTransaction
        {
            get { return _NewTransaction; }
            set { _NewTransaction = value; ; OnPropertyChanged(); }
        }


        private ObservableCollection<Transaction> _Transactions;
        public ObservableCollection<Transaction> Transactions
        {
            get { return _Transactions; }
            set { _Transactions = value; OnPropertyChanged(); OnPropertyChanged("Accounts"); }
        }
/* Attempting to implement SelectedItem binding
        private Account _CurrentAccount;
        public Account CurrentAccount 
        {   
            get {return _CurrentAccount;}
            set { _CurrentAccount = value; OnPropertyChanged(); }
        }

        public IEnumerable<Transaction> CurrentTransactions
        {
            get { return Transactions.Where(t => t.Account.Name == _CurrentAccount);}
        }
*/

        private ObservableCollection<Account> _Accounts;
        public ObservableCollection<Account> Accounts
        {
            get { return _Accounts; }
            set { _Accounts = value; OnPropertyChanged(); }
        }

        private DelegateCommand _SaveTransaction;
        public ICommand SaveTransaction
        {
            get
            {
                if (_SaveTransaction == null)
                {
                    _SaveTransaction = new DelegateCommand
                    {
                        ExecuteFunction = x =>
                          {
                              _Db.Transactions.Add(_NewTransaction);
                              Account updateAccount = (from A in Accounts where A == _NewTransaction.Account select A).Single();
                              updateAccount.Balance += _NewTransaction.Amount;
                              _Db.SaveChanges(); NewTransaction = new Transaction { Date = DateTime.Now };

                          },
                        CanExecuteFunction = x => NewTransaction.Amount != 0 && NewTransaction.Account != null
                    };
                    _NewTransaction.PropertyChanged += (s, e) => _SaveTransaction.OnCanExecuteChanged();
                }
                return _SaveTransaction;
            }
            
        }

        public DelegateCommand Open_Account
        {
            get
            {
                
                    return new DelegateCommand
                    {
                        ExecuteFunction = _ => { NewAccountWindow NewAccountWin = new NewAccountWindow(); 
                                                 NewAccountWin.ShowDialog(); 
                                                 var accountVM = NewAccountWin.DataContext as AccountVM; 
                                                 if(accountVM.isSave ==true) Fill();}
                    };
                
            }
        }

        public void Fill()
        {
            Transactions = _Db.Transactions.Local;
            Accounts = _Db.Accounts.Local;
            _Db.Accounts.ToList();
            _Db.Transactions.ToList();
        }

    }
}


