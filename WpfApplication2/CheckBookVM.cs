
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

        private int _RowsPerPage = 5;
        private int _CurrentPage = 1;
        public int CurrentPage
        {
            get { return _CurrentPage; }
            set { _CurrentPage = value; OnPropertyChanged(); OnPropertyChanged("CurrentTransactions"); }
        }

        private ObservableCollection<Transaction> _Transactions;
        public ObservableCollection<Transaction> Transactions
        {
            get { return _Transactions; }
            set { _Transactions = value; OnPropertyChanged(); OnPropertyChanged("Accounts"); }
        }

        public IEnumerable<Account> Accounts
        {
            get { return _Db.Accounts.Local; }
        }

        public IEnumerable<Transaction> CurrentTransactions
        {
            get { return Transactions.Skip((_CurrentPage - 1) * _RowsPerPage).Take(_RowsPerPage); }
        }

        public DelegateCommand MoveNext
        {
            get
            {
                return new DelegateCommand {
                    ExecuteFunction = _ => CurrentPage++,
                    CanExecuteFunction = _ => CurrentPage * _RowsPerPage < Transactions.Count
                };
            }
        }

        public DelegateCommand MoveLast
        {
            get
            {
                return new DelegateCommand
                {
                    ExecuteFunction = _ => CurrentPage--,
                    CanExecuteFunction = _ => CurrentPage * _RowsPerPage < Transactions.Count
                };
            }
        }


        public DelegateCommand Save
        {
       
            get
            {
                return new DelegateCommand 
                {
                    ExecuteFunction = _ => {Transactions.Add(new Transaction( ));}
                };
            }
        }

        public DelegateCommand Open_Transaction
        {
            get
            {
                
                    return new DelegateCommand
                    {
                        ExecuteFunction = _ => { NewTransactionWindow NewTranWin = new NewTransactionWindow(); 
                                                 NewTranWin.ShowDialog(); 
                                                 var TransVM = NewTranWin.DataContext as TransactionVM; 
                                                 if(TransVM.isSave ==true)Transactions.Add((Transaction)TransVM.Data);},
                       CanExecuteFunction = _ => Accounts.Count() > 0
                    };
            }
        }

        public void Fill()
        {
            Transactions = _Db.Transactions.Local;
            _Db.Accounts.ToList();
            _Db.Transactions.ToList();
        }

    }
}


