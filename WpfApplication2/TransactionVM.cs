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
            set { _Data = value; }
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
                    ExecuteFunction = _ => _Db.SaveChanges(),
                    CanExecuteFunction = _ => _Db.ChangeTracker.HasChanges()
                };
            }
        }


    }
}