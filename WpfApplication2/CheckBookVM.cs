
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;

namespace CSharpLaPierre
{

    public class CheckBookVM : BaseVM
    {
        public CheckBookVM()
        {
        }

        CbDb _Db = new CbDb();

        public Transaction _NewTransaction = new Transaction { Date = DateTime.Now, Transactee=""};
        public Transaction NewTransaction
        {
            get { return _NewTransaction; }
            set { _NewTransaction = value; OnPropertyChanged(); OnPropertyChanged("CheckHighestPayee"); }
        }


        private ObservableCollection<Transaction> _Transactions;
        public ObservableCollection<Transaction> Transactions
        {
            get { return _Transactions; }
            set { _Transactions = value; OnPropertyChanged(); OnPropertyChanged("CurrentTransactions"); OnPropertyChanged("Accounts"); }
        }

        private Account _CurrentAccount;
        public Account CurrentAccount 
        {   
            get {return _CurrentAccount;}
            set { _CurrentAccount = value; OnPropertyChanged(); OnPropertyChanged("CurrentTransactions");}
        }


        public ObservableCollection<Transaction> CurrentTransactions 
        {
            get { return new ObservableCollection<Transaction>(Transactions.Where(t => t.Account == _CurrentAccount).ToList()); }
        }

        private Transaction _CurrentTransaction;
        public Transaction CurrentTransaction
        {
            get { return _CurrentTransaction; }
            set { _CurrentTransaction = value; OnPropertyChanged(); }
        }

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
                              Account updateAccount = (from A in Accounts where A == _NewTransaction.Account select A).Single();
                              updateAccount.Balance += _NewTransaction.Amount;
                              _Db.Transactions.Add(_NewTransaction);
                              _Db.SaveChanges(); Fill(); NewTransaction = new Transaction { Date = DateTime.Now,Transactee="" }; _CurrentAccount = CurrentAccount;


                          },
                        CanExecuteFunction = x => NewTransaction.Amount != 0 && NewTransaction.Account != null
                    };
                    _NewTransaction.PropertyChanged += (s, e) => _SaveTransaction.OnCanExecuteChanged();
                }
                return _SaveTransaction;
            }
            
        }


        public DelegateCommand DeleteTransaction
        {
            get
            {
                    return new DelegateCommand
                    {
                        ExecuteFunction = _ =>
                        {
                            Account updateAccount = (from A in Accounts where A == _CurrentTransaction.Account select A).Single();
                            updateAccount.Balance -= _CurrentTransaction.Amount;
                            _Db.Transactions.Remove(_CurrentTransaction);
                            _Db.SaveChanges(); Fill();
                        }
                    };

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
        private bool _CheckHighestPayee;
        public bool CheckHighestPayee
        {
            get
            {
                return _CheckHighestPayee;
            }
            set
            {
               _CheckHighestPayee = _NewTransaction.Transactee == Transactions.GroupBy(t => t.Transactee).Select(a => new { a.Key, Sum = a.Sum(t => t.Amount) }).Max().Key; OnPropertyChanged(); OnPropertyChanged("Background");
            }

        }

        public Brush Background
        {
            get
            {
                return _CheckHighestPayee ? Brushes.Red : Brushes.White;
            }
        }

        public string HighestPayee
        {

            get 
            { 
                return (Transactions.GroupBy(t=> t.Transactee).Select(a => new { a.Key, Sum=a.Sum(t => t.Amount)}).Max().Key);
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


