using BankingSystemMVC.Models;
using BankingSystemMVC.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Data.Entity;
using System.Transactions;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;
using IsolationLevel = BankingSystemMVC.UnitOfWork.IsolationLevel;
using System.Text.Json;
using System.Configuration;

namespace UnitTests___Banking
{
    [TestClass]
    public class AccountTableTests
    {

        IsolationLevel isolation;
        [TestInitialize]
        public void Initialize()
        {
            var path = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath;
            isolation = new IsolationLevel();
            isolation.SetIsolationLevel();
        }

        [TestCleanup]
        public void Cleanup()
        {
        }



        [TestMethod]
        public void InsertUpdateDelete() 
        {
            UnitOfWorkBank uow = new UnitOfWorkBank();
            CardBrand card = new CardBrand
            {
                Name = "Test"
            };
            uow.CardBrandRepository.Insert(card);
            uow.Save();
            var cardbrand = uow.CardBrandRepository.Get((cardbrand) => cardbrand.Name == "Test").FirstOrDefault();
            Assert.AreEqual(cardbrand.Name, "Test");
            cardbrand.Name = "Test2";
            uow.CardBrandRepository.Update(cardbrand);
            uow.Save();
            cardbrand = uow.CardBrandRepository.Get((cardbrand) => cardbrand.Name == "Test2").FirstOrDefault();
            Assert.AreEqual(cardbrand.Name, "Test2");
            uow.CardBrandRepository.Delete(cardbrand);
            uow.Save();
            cardbrand = uow.CardBrandRepository.Get((cardbrand) => cardbrand.Name == "Test2").FirstOrDefault();
            Assert.IsNull(cardbrand);
        }


        [TestMethod]
        public void DirtyRead()
        {

            UnitOfWorkBank uow = new UnitOfWorkBank();
            UnitOfWorkBank uow2 = new UnitOfWorkBank();

            decimal previousAccountBalance;

            using (IDbContextTransaction transaction = uow.context.Database.BeginTransaction(isolation.GetIsolationLevel()))
            {
                var account = uow.AccountRepository.GetByID(1);
                previousAccountBalance = account.Balance;
                account.Balance = 123;
                uow.AccountRepository.Update(account);
                uow.Save();

                using (IDbContextTransaction transaction2 = uow2.context.Database.BeginTransaction(isolation.GetIsolationLevel()))
                {
                    //dirty read
                    var account2 = uow2.AccountRepository.GetByID(1);
                    var accountBalance2 = account2.Balance;
                    Assert.AreEqual(123, accountBalance2);
                    
                }
                transaction.Rollback();
            }

            //account balance is back to original amount 
            uow = new UnitOfWorkBank();

            using (IDbContextTransaction transaction = uow.context.Database.BeginTransaction(isolation.GetIsolationLevel()))
            {
                var accountAfterRollback = uow.AccountRepository.GetByID(1);
                decimal accountBalanceAfterRollback;
                accountBalanceAfterRollback = accountAfterRollback.Balance;
                Assert.AreEqual(accountBalanceAfterRollback, previousAccountBalance);
                transaction.Rollback();
            }
        }

        [TestMethod]
        public void NonRepeatableRead()
        {
            UnitOfWorkBank uow = new UnitOfWorkBank();
            UnitOfWorkBank uow2 = new UnitOfWorkBank();

            using (IDbContextTransaction transaction = uow.context.Database.BeginTransaction(isolation.GetIsolationLevel()))
            {
                var account = uow.AccountRepository.GetByID(1);
                decimal balanceBefore = account.Balance;

                using (IDbContextTransaction transaction2 = uow2.context.Database.BeginTransaction(isolation.GetIsolationLevel()))
                {
                    var account2 = uow2.AccountRepository.GetByID(1);
                    account2.Balance += 1;
                    uow2.Save();
                    transaction2.Commit();

                }

                uow.context.Entry(account).Reload();
                decimal balanceAfter = account.Balance;

                Assert.AreEqual(balanceAfter, balanceBefore + 1);
                Assert.AreNotEqual(balanceAfter, balanceBefore);

                transaction.Rollback();

            }

        }


        [TestMethod]
        public void PhantomRead()
        {
            UnitOfWorkBank uow = new UnitOfWorkBank();
            UnitOfWorkBank uow2 = new UnitOfWorkBank();
            Account testAccount;

            using (IDbContextTransaction transaction = uow.context.Database.BeginTransaction(isolation.GetIsolationLevel()))
            {
                int accountCountBefore = uow.AccountRepository.GetAll().ToList().Count;

                using (IDbContextTransaction transaction2 = uow2.context.Database.BeginTransaction(isolation.GetIsolationLevel()))
                {
                    testAccount = new Account
                    {
                        AccountNumber = 99999,
                        SavingsAccountTypeId = 1,
                        PersonalAccountTypeId = null,
                        CustomerId = 1,
                        CurrencyId = 1,
                        Balance = 12345
                    };
                    uow2.AccountRepository.Insert(testAccount);
                    uow2.Save();
                    transaction2.Commit();
                }
                //phantom read
                int accountCountAfter = uow.AccountRepository.GetAll().ToList().Count;


                Assert.AreNotEqual(accountCountAfter, accountCountBefore);


            }

            uow2.AccountRepository.Delete(testAccount);
            uow2.Save();



        }
    }
}