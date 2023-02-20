using BankingSystemMVC.Models;

namespace BankingSystemMVC.ViewModels
{
    public class CreateMeetingViewModel
    {

        public List<Customer>? Customers { get; set; }
        public List<Employee>? Employees { get; set; }

        public Meeting Meeting { get; set; }
    
    
    
    }
}
