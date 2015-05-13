using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLaPierre
{
    public class Transaction : BaseVM
    {
        public int Id { get; set; }

        private DateTime _Date = DateTime.Now;
        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; OnPropertyChanged(); }
        }

        private string _Transactee;
        public string Transactee
        {
            get { return _Transactee; }
            set { _Transactee = value; OnPropertyChanged(); }
        }

        private Account _Account;
        public virtual Account Account
        {
            get { return _Account; }
            set { _Account = value; OnPropertyChanged(); }
        }

        private decimal _Amount;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; OnPropertyChanged(); }
        }

        private string _Tag;

        public string Tag
        {
            get { return _Tag; }
            set { _Tag = value; OnPropertyChanged(); }
        }

    }

    public class Account : BaseVM
    {
        public int Id { get; set; }
        private string _Name;
        public string Name 
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged(); }
        }
        private string _Institution;
        public string Institution
        {
            get { return _Institution; }
            set { _Institution = value; OnPropertyChanged(); }
        }
        private decimal _Balance;
        public decimal Balance
        {
            get { return _Balance; }
            set { _Balance = value; OnPropertyChanged(); }
        }
    }
}
