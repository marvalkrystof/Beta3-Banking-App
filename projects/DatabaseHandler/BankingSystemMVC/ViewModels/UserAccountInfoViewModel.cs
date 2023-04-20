using BankingSystemMVC.Models;

namespace BankingSystemMVC.ViewModels
{
    /// <summary>
    /// Model used for passing info to the UserAccountInfo view
    /// </summary>
    public class UserAccountInfoViewModel
    {
        public UserAccount userAccount 
        { 
            get; 
            set;
        }
        public List<Role> roles 
        { 
            get;
            set;
        }
    }
}
