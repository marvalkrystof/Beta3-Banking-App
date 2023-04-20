namespace BankingSystemMVC.Models
{

    /// <summary>
    /// Model used for role assignement, when receiving JSON with role data from RoleManagement
    /// </summary>
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
