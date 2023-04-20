using BankingSystemMVC.Models;
using BankingSystemMVC.RoleManagement;
using BankingSystemMVC.UnitOfWork;
using BankingSystemMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystemMVC.Controllers
{
/// <summary>
/// Serves actions used for user web app account
/// </summary>
    public class User_AccountController : Controller
    {
        private UnitOfWorkBank uow = new UnitOfWorkBank();
        /// <summary>
        /// Serves information about user web account information
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                return RedirectToAction("NoPermission","Home");
            }
            UserAccount userAccount = uow.UserAccountRepository.Get((account) => account.Username.Equals(HttpContext.Session.GetString("Username"))).FirstOrDefault();
            UserAccountInfoViewModel accountInfoViewModel = new UserAccountInfoViewModel
            {
                userAccount = userAccount,
                roles = UserRoleList.GetUserRoles(userAccount)
            };

            return View(accountInfoViewModel);
        }
        

    }
}
