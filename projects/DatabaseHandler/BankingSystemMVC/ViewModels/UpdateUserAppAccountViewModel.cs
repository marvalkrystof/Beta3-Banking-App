using BankingSystemMVC.Models;

namespace BankingSystemMVC.ViewModels
{
    public class UpdateUserAppAccountViewModel
    {
        public UserAccount account 
        { 
            get; 
            set;
        }
       
        public List<Customer>? customers 
        { 
            get;
            set;
        }
        public List<Employee>? employees 
        { 
            get; 
            set;
        } 
    }
}
