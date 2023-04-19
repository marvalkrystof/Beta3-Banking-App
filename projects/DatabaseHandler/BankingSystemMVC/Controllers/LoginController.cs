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
    public class LoginController : Controller
    {
        UnitOfWorkBank uow = new UnitOfWorkBank();

        public IActionResult Index()
        {
            return View();
        }
     
        public IActionResult Login()
        {
            if(!string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                return RedirectToAction("AlreadyLoggedIn");
            }
            return View();
        }

        
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
        public IActionResult FailedLogin()
        {
            return View();
        }
        
        public IActionResult AlreadyLoggedIn()
        {
            return View();
        }
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
