using BankingSystemMVC.Models;

namespace BankingSystemMVC.ViewModels
{
    /// <summary>
    /// View model used for displaying account information
    /// </summary>
    public class AccountInfoViewModel
    {

        public Account Account 
        { 
            get; 
            set; 
        }
        public List<Card> Cards 
        { 
            get; 
            set; 
        }
    
    
    }
}
