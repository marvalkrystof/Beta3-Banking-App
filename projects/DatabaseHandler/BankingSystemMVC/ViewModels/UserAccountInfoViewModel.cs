using BankingSystemMVC.Models;

namespace BankingSystemMVC.ViewModels
{
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
