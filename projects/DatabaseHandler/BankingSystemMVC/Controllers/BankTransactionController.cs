
using BankingSystemMVC.Models;
using BankingSystemMVC.RoleManagement;
using BankingSystemMVC.UnitOfWork;
using BankingSystemMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

namespace BankingSystemMVC.Controllers
{
    /// <summary>
    /// Controller that controls the actions for the bank transactions.
    /// </summary>
    public class BankTransactionController : Controller
    {
        private UnitOfWorkBank unitOfWork = new UnitOfWorkBank();
  

        /// <summary>
        /// Serves the view with data for creating the bank transaction
        /// </summary>
        /// <returns>The view</returns>
        public IActionResult CreateTransaction()
        {

            var allAccounts = unitOfWork.AccountRepository.GetAll().ToList();
            if (PermissionChecker.hasPermission(HttpContext.Session, "employee"))
            {
                CreateTransactionViewModel model = new CreateTransactionViewModel
                {
                    UserAccounts = allAccounts,
                    Accounts = allAccounts
                };
                return View(model);
            }
            else if (PermissionChecker.hasPermission(HttpContext.Session, "customer"))
            {
                var sessionUsername = HttpContext.Session.GetString("Username");
                var appAccount = unitOfWork.UserAccountRepository.Get((userAccount) => userAccount.Username == sessionUsername).FirstOrDefault();
                var accounts = unitOfWork.AccountRepository.Get((account) => account.CustomerId == appAccount.CustomerId).ToList();

                CreateTransactionViewModel model = new CreateTransactionViewModel
                {
                    UserAccounts = accounts,
                    Accounts = allAccounts
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("NoPermission","Home");
            }
        }
       
        /// <summary>
        /// Handles the post request of the createtransaction action and saves it into the db, does the conversion rate
        /// </summary>
        /// <param name="model">Create treansaction view model</param>
        /// <returns>Redirect</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTransaction(CreateTransactionViewModel model)
        {
            if (ModelState.IsValid)
            {

                BankTransaction transaction = new BankTransaction
                {
                    Amount = model.Transaction.Amount,
                    Note = model.Transaction.Note,
                    ToAccountId = model.Transaction.ToAccountId,
                    FromAccountId = model.Transaction.FromAccountId,
                    TransactionDate = DateTime.Today
                };

                var receivingAccount = unitOfWork.AccountRepository.GetByID(transaction.ToAccountId);
                var sendingAccount = unitOfWork.AccountRepository.GetByID(transaction.FromAccountId);

                if (sendingAccount.Balance < transaction.Amount)
                {
                    return RedirectToAction("NotEnoughMoney");
                }

                var amountToAdd = ConvertCurrency(transaction.Amount, sendingAccount.Currency.UsdConversionRate, receivingAccount.Currency.UsdConversionRate);

                sendingAccount.Balance -= transaction.Amount;
                receivingAccount.Balance += amountToAdd;

                unitOfWork.AccountRepository.Update(sendingAccount);
                unitOfWork.AccountRepository.Update(receivingAccount);

                unitOfWork.BankTransactionRepository.Insert(transaction);
                unitOfWork.Save();

                return RedirectToAction("CreateTransaction");
            } 
            else
            {
                return RedirectToAction("", "Home");
            }

        
        }
        /// <summary>
        /// Returned if you don't have enough money on the selected account
        /// </summary>
        /// <returns>The view</returns>
        public IActionResult NotEnoughMoney()
        {
            return View();
        }

        /// <summary>
        /// Converts one currency to the other
        /// </summary>
        /// <param name="amount">Amount of money</param>
        /// <param name="usdConversionRateSender">Usd conversion rate of the currency of the sender</param>
        /// <param name="usdConvertsionRateReceiver">Usd conversion rate of the currency of the receiver</param>
        /// <returns>Converted currency</returns>
        private decimal ConvertCurrency(decimal amount,decimal usdConversionRateSender, decimal usdConvertsionRateReceiver )
        {
            return Math.Round((amount / usdConversionRateSender) * usdConvertsionRateReceiver,2);
        }
    }

}
