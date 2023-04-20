using BankingSystemMVC.Models;

namespace BankingSystemMVC.RoleManagement
{
    /// <summary>
    /// Used for checking the role permissions to different controllers
    /// </summary>
    public class PermissionChecker
    { 
    
         /// <summary>
         /// Checks whether user has specific role 
         /// </summary>
         /// <param name="session">User session</param>
         /// <param name="role">Role to check</param>
         /// <returns>True if user has permisison</returns>
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
