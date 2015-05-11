using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLaPierre
{

    class TransactionVM : BaseVM
    {
        CbDb _Db = new CbDb();

        private Transaction _Data = new Transaction { Date = DateTime.Now } ;
        public Transaction Data
        {
            get { return _Data; }
            set { _Data = value; ; OnPropertyChanged(); }
        }

        public IEnumerable<Account> Accounts
        {
            get { return _Db.Accounts.ToList(); }
            //set { _Accounts = value; }
        }

        public DelegateCommand Save
        {
            get
            {
                return new DelegateCommand
                {
                    ExecuteFunction = _ => { _Db.Transactions.Add(_Data);
                                             Data.Account.Balance = Data.Account.Balance + Data.Amount;
                                             _Db.SaveChanges();},
                    CanExecuteFunction = _ => !string.IsNullOrEmpty(_Data.Payee)
                };
            }
        }


    }
}