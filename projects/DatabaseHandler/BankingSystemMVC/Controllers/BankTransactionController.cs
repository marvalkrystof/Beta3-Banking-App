
using BankingSystemMVC.Models;
using BankingSystemMVC.RoleManagement;
using BankingSystemMVC.UnitOfWork;
using BankingSystemMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

namespace BankingSystemMVC.Controllers
{
    public class BankTransactionController : Controller
    {
        private UnitOfWorkBank unitOfWork = new UnitOfWorkBank();
  

        
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

        public IActionResult NotEnoughMoney()
        {
            return View();
        }

        private decimal ConvertCurrency(decimal amount,decimal usdConversionRateSender, decimal usdConvertsionRateReceiver )
        {
            return Math.Round((amount / usdConversionRateSender) * usdConvertsionRateReceiver,2);
        }
    }

}
