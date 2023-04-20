
using BankingSystemMVC.Models;

namespace BankingSystemMVC.ViewModels
{
   /// <summary>
   /// Model used for passing info to the CreateTransaction view
   /// </summary>
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
