using BankingSystemMVC.Models;
using BankingSystemMVC.RoleManagement;
using BankingSystemMVC.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using SharpHash.Base;
using SharpHash.Interfaces;
using System.Text;

namespace BankingSystemMVC.Controllers
{
    /// <summary>
    /// Handles the authentication of the site
    /// </summary>
    public class LoginController : Controller
    {
        UnitOfWorkBank uow = new UnitOfWorkBank();

        
        /// <summary>
        /// Login form for the login
        /// </summary>
        /// <returns>The view</returns>
        public IActionResult Login()
        {
            if(!string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                return RedirectToAction("AlreadyLoggedIn");
            }
            return View();
        }

        
        /// <summary>
        /// Handles the post of the login form and logs in the user and stores the session
        /// </summary>
        /// <param name="userAccount">User Account</param>
        /// <returns>Redirect</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                UserAccount RetrievedAccount = uow.UserAccountRepository.Get((account) => account.Username == userAccount.Username).FirstOrDefault();

                if(RetrievedAccount == null)
                {
                    return RedirectToAction("FailedLogin");
                }

                IHash hash = HashFactory.Crypto.CreateMD5();
                hash.Initialize();
                hash.TransformString(userAccount.Password, Encoding.UTF8);
                string hashedPass = hash.TransformFinal().ToString();

                
                if (!RetrievedAccount.Password.ToLower().Equals(hashedPass.ToLower()))
                {
                    return RedirectToAction("FailedLogin");
                }

                if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
                {
                    List<Role> userRoles = UserRoleList.GetUserRoles(RetrievedAccount);
                    string rolesJson = UserRoleList.Serialize(userRoles);
                    HttpContext.Session.SetString("Roles", rolesJson);
                    HttpContext.Session.SetString("Username", userAccount.Username);
                    
                }
                return RedirectToAction("", "Home");
            }
            else
            {
                return RedirectToAction("FailedLogin");
            }

        }
        /// <summary>
        /// View returned when login failed for any reason
        /// </summary>
        /// <returns>View</returns>
        public IActionResult FailedLogin()
        {
            return View();
        }
        
        /// <summary>
        /// Returned if user is already logged in and call the login action
        /// </summary>
        /// <returns>View</returns>
        public IActionResult AlreadyLoggedIn()
        {
            return View();
        }

        /// <summary>
        /// Called when user logs out, deletes his sessions
        /// </summary>
        /// <returns>View</returns>
        public IActionResult LogOut()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                HttpContext.Session.SetString("Username", "");
                HttpContext.Session.SetString("Roles","");
            }
            return RedirectToAction("","Home");
        }

        }
}
