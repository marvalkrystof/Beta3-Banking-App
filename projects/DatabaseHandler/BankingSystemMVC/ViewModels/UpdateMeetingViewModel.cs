using BankingSystemMVC.Models;

namespace BankingSystemMVC.ViewModels
{

    /// <summary>
    /// Model used for passing info to UpdateMeeting view
    /// </summary>
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
