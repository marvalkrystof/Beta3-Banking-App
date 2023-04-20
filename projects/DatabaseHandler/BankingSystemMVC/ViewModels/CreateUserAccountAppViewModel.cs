using BankingSystemMVC.Models;

namespace BankingSystemMVC.ViewModels
{
    /// <summary>
    /// Model used for passing information to CreateUserAccount view
    /// </summary>
    public class CreateUserAccountAppViewModel
    {

        public List<Customer>? Customers 
        { 
            get; 
            set;
        }
        public List<Employee>? Employees 
        { 
            get; 
            set;
        }

        public UserAccount UserAccount 
        { 
            get; 
            set; 
        }



    }
}
