using BankingSystemMVC.Models;
using BankingSystemMVC.UnitOfWork;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Text.Json;
using System.Reflection.PortableExecutable;

namespace BankingSystemMVC.RoleManagement
{
    public class UserRoleList
    {
        public static List<Role> GetUserRoles(UserAccount userAccount)
        {
            List<Role> roles = new List<Role>();
            foreach (AccountRole accountRole in userAccount.AccountRoles.ToList())
            {
                roles.Add(accountRole.Role);
            }
            return roles;
        }

        public static string Serialize(List<Role> userRoles) {
            string json = JsonSerializer.Serialize(userRoles);
            return json;
        }
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
