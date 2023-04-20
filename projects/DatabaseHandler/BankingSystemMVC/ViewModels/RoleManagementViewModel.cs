using BankingSystemMVC.Models;

namespace BankingSystemMVC.ViewModels
{

    /// <summary>
    /// Model used for passing info to role management view
    /// </summary>
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
