using BankingSystemMVC.Models;
using BankingSystemMVC.RoleManagement;
using BankingSystemMVC.UnitOfWork;
using BankingSystemMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace BankingSystemMVC.Controllers
{

/// <summary>
/// Controller that serves information about bank accounts
/// </summary>
    public class AccountController : Controller
    {
        private UnitOfWorkBank unitOfWork = new UnitOfWorkBank();

        /// <summary>
        /// Returns accounts available to the user
        /// </summary>
        /// <returns>View with the accounts select</returns>
        public IActionResult Accounts()
        {
            if (PermissionChecker.hasPermission(HttpContext.Session, "employee"))
            {
                var accounts = unitOfWork.AccountRepository.GetAll();
                return View(accounts);
            }
            else if (PermissionChecker.hasPermission(HttpContext.Session, "customer"))
            {
                var sessionUsername = HttpContext.Session.GetString("Username");
                var appAccount = unitOfWork.UserAccountRepository.Get((userAccount) => userAccount.Username == sessionUsername).FirstOrDefault();
                var accounts = unitOfWork.AccountRepository.Get((account) => account.CustomerId == appAccount.CustomerId);
                return View(accounts);
            }
            else
            {
                return RedirectToAction("NoPermission","Home");
            }
            
        }

        /// <summary>
        /// Serves the information of aspecific account
        /// </summary>
        /// <param name="id">Account id</param>
        /// <returns>Partial View with the accounts</returns>
        public PartialViewResult AccountInfo(int id)
        {
            if (PermissionChecker.hasPermission(HttpContext.Session, "customer") || PermissionChecker.hasPermission(HttpContext.Session, "employee"))
            {
                AccountInfoViewModel viewModel = new AccountInfoViewModel
                {
                    Account = unitOfWork.AccountRepository
                    .Get((account) => account.Id == id,
                     includeProperties: "BankTransactionFromAccounts.ToAccount.Customer,BankTransactionToAccounts.FromAccount.Customer,Currency,Customer,PersonalAccountType,SavingsAccountType").FirstOrDefault(),
                    Cards = unitOfWork.CardRepository.Get((card) => card.AccountId == id, includeProperties: "CardBrand").ToList()
                };

                return PartialView(viewModel);
            } 
            else
            {
                return PartialView("NoPermission", "Home");
            }
        }
    }
}
