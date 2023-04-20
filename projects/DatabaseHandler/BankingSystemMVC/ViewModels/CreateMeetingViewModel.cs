using BankingSystemMVC.Models;

namespace BankingSystemMVC.ViewModels
{
    /// <summary>
    /// Model used for displaying data in the Create Meeting view
    /// </summary>
    public class CreateMeetingViewModel
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

        public Meeting Meeting 
        { 
            get; 
            set; 
        }
    
    
    
    }
}
