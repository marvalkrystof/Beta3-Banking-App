namespace BankingSystemMVC.Models
{
    public class RoleAssignmentModel
    {

        public string userId 
        { 
            get; 
            set;
        }
        public List<string> valuesAdded  
        { 
            get; 
            set;
        }
        public List<string> valuesRemoved 
        { 
            get; 
            set;
        }


    }
}
