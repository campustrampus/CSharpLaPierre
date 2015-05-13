using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLaPierre
{
    class AccountVM : BaseVM
    {
        CbDb _Db = new CbDb();
        public bool isSave = false;

        private Account _Data = new Account { };
        public Account Data
        {
            get { return _Data; }
            set { _Data = value; OnPropertyChanged(); }
        }

        public DelegateCommand Save
        {
            get
            {
                return new DelegateCommand
                {
                    ExecuteFunction = _ => { _Db.Accounts.Add(_Data);
                                             _Db.SaveChanges(); isSave=true;},
                    //CanExecuteFunction = _ => !string.IsNullOrEmpty()
                };
            }
        }
    }
}