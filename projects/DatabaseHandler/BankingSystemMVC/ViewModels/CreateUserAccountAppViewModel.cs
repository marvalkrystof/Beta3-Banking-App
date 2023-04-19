using BankingSystemMVC.Models;

namespace BankingSystemMVC.ViewModels
{
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
