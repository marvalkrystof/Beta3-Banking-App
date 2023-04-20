using BankingSystemMVC.Models;
using BankingSystemMVC.UnitOfWork;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Text.Json;
using System.Reflection.PortableExecutable;

namespace BankingSystemMVC.RoleManagement
{
    /// <summary>
    /// Used for operations with roles tied to specific user account
    /// </summary>
    public class UserRoleList
    {
        /// <summary>
        /// Gets the list of user account roles tied to an account 
        /// </summary>
        /// <param name="userAccount">User account</param>
        /// <returns>List of roles tied to account</returns>
        public static List<Role> GetUserRoles(UserAccount userAccount)
        {
            List<Role> roles = new List<Role>();
            foreach (AccountRole accountRole in userAccount.AccountRoles.ToList())
            {
                roles.Add(accountRole.Role);
            }
            return roles;
        }

        /// <summary>
        /// Serializes the list of roles into JSON format
        /// </summary>
        /// <param name="userRoles">List of roles</param>
        /// <returns>Json of roles</returns>
        public static string Serialize(List<Role> userRoles) {
            string json = JsonSerializer.Serialize(userRoles);
            return json;
        }
        /// <summary>
        /// Deserializes the list of roles from Json format
        /// </summary>
        /// <param name="userRoleJson">Json of user roles</param>
        /// <returns>List of roles</returns>
        public static List<Role> Deserialize(string userRoleJson)
        {
            if(string.IsNullOrEmpty(userRoleJson))
            {
                return new List<Role>();
            }
            return JsonSerializer.Deserialize<List<Role>>(userRoleJson);
        }
       
    }
}
