using BankingSystemMVC.Models;

namespace BankingSystemMVC.ViewModels
{
    public class UpdateMeetingViewModel
    {
        public Meeting meeting 
        { 
            get;
            set;
        }
        public List<Employee>? employees
        {
            get;
            set;
        }
        public List<Customer>? customers 
        {
            get;
            set;
        }

    }
}
