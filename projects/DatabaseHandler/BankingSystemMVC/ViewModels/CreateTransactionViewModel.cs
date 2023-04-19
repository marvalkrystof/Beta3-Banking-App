
using BankingSystemMVC.Models;

namespace BankingSystemMVC.ViewModels
{
    public class CreateTransactionViewModel
    {

        public BankTransaction Transaction 
        { 
            get; 
            set;
        }
        public List<Account>? UserAccounts 
        {
            get; 
            set;
        }
        public List<Account>? Accounts  
        {
            get;
            set;
        }


    }
}
