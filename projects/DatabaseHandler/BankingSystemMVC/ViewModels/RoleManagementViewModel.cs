using BankingSystemMVC.Models;

namespace BankingSystemMVC.ViewModels
{
    public class RoleManagementViewModel
    {

        public int userId 
        { 
            get; 
            set;
        }
        public List<Role> unassignedRoles 
        { 
            get; 
            set;
        }
        public List<Role> assignedRoles 
        { 
            get; 
            set;
        }





    }
}
