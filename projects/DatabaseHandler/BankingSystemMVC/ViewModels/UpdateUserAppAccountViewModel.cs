using BankingSystemMVC.Models;

namespace BankingSystemMVC.ViewModels
{
    /// <summary>
    /// Model used for passing information to the UpdateUserAccount view
    /// </summary>
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
