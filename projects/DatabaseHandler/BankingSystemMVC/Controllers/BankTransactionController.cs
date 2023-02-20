
using BankingSystemMVC.Models;
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
            CreateTransactionViewModel model = new CreateTransactionViewModel
            {
                Accounts = unitOfWork.AccountRepository.GetAll().ToList()
            };
           

            return View(model);
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
            } else
            {
                return RedirectToAction("/Home/");
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
