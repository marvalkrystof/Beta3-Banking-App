using BankingSystemMVC.Models;
using BankingSystemMVC.UnitOfWork;
using BankingSystemMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace BankingSystemMVC.Controllers
{
    public class AccountController : Controller
    {
        private UnitOfWorkBank unitOfWork = new UnitOfWorkBank();
        public IActionResult Accounts()
        {
            var accounts = unitOfWork.AccountRepository.GetAll();
            return View(accounts);
        }


        public PartialViewResult AccountInfo(int id)
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
    }
}
