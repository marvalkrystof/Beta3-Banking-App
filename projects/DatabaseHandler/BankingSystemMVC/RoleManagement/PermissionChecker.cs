using BankingSystemMVC.Models;

namespace BankingSystemMVC.RoleManagement
{
    public class PermissionChecker
    { 
    
         
        public static bool hasPermission(ISession session, string role)
        {
 
            string rolesJson = session.GetString("Roles");
            List<Role> roles = UserRoleList.Deserialize(rolesJson);
            foreach (Role roleName in roles)
            {
                if (role == roleName.Name)
                {
                    return true;
                }
            }
            return false;

        }
    }
}
