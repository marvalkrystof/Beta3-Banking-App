using BankingSystemMVC.Models;
using BankingSystemMVC.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystemMVC.UnitOfWork
{
    public class UnitOfWorkBank : IUnitOfWork
    {

        // Repositories for each table in the Db

        public BankingSystemDbContext context = new BankingSystemDbContext();
        private Repository<Account> accountRepository;
        private Repository<BankTransaction> bankTransactionRepository;
        private Repository<Card> cardRepository;
        private Repository<CardBrand> cardBrandRepository;
        private Repository<Currency> currencyRepository;
        private Repository<Customer> customerRepository;
        private Repository<Employee> employeeRepository;
        private Repository<Meeting> meetingRepository;
        private Repository<PersonalAccountType> personalAccountTypeRepository;
        private Repository<SavingsAccountType> savingAccountTypeRepository;
        private Repository<UserAccount> userAccountRepository;
        private Repository<Role> roleRepository;
        private Repository<AccountRole> accountRoleRepository;



 
        public UnitOfWorkBank()
        {

        }

        //Singletons for the repositories
        public Repository<Account> AccountRepository { 
            
            get {
                if (this.accountRepository == null)
                    {
                        this.accountRepository = new Repository<Account>(context);
                    }
                    return accountRepository;
            }
          }
        public Repository<BankTransaction> BankTransactionRepository {
            get
            {
                if (this.bankTransactionRepository == null)
                {
                    this.bankTransactionRepository = new Repository<BankTransaction>(context);
                }
                return bankTransactionRepository;
            }
        }
        public Repository<Card> CardRepository {
            get
            {
                if (this.cardRepository == null)
                {
                    this.cardRepository = new Repository<Card>(context);
                }
                return cardRepository;
            }
        }
        public Repository<CardBrand> CardBrandRepository {
            get
            {
                if (this.cardBrandRepository == null)
                {
                    this.cardBrandRepository = new Repository<CardBrand>(context);
                }
                return cardBrandRepository;
            }
        }
        public Repository<Currency> CurrencyRepository {
            get
            { 
                if (this.currencyRepository == null)
                {
                    this.currencyRepository = new Repository<Currency>(context);
                }
                return currencyRepository;
            }
        }
        public Repository<Customer> CustomerRepository {
            get
            {
                if (this.customerRepository == null)
                {
                    this.customerRepository = new Repository<Customer>(context);
                }
                return customerRepository;
            }
        }
        public Repository<Employee> EmployeeRepository {
            get
            {
                if (this.employeeRepository == null)
                {
                    this.employeeRepository = new Repository<Employee>(context);
                }
                return employeeRepository;
            }
        }
        public Repository<Meeting> MeetingRepository {
            get
            {
                if (this.meetingRepository == null)
                {
                    this.meetingRepository = new Repository<Meeting>(context);
                }
                return meetingRepository;
            }
        }
        public Repository<PersonalAccountType> PersonalAccountTypeRepository {
            get
            {
                if (this.personalAccountTypeRepository == null)
                {
                    this.personalAccountTypeRepository = new Repository<PersonalAccountType>(context);
                }
                return personalAccountTypeRepository;
            }
        }
        public Repository<SavingsAccountType> SavingAccountTypeRepository {
            get
            {
                if (this.savingAccountTypeRepository == null)
                {
                    this.savingAccountTypeRepository = new Repository<SavingsAccountType>(context);
                }
                return savingAccountTypeRepository;
            }
        }

        public Repository<AccountRole> AccountRoleRepository
        {
            get
            {
                if (this.accountRoleRepository == null)
                {
                    this.accountRoleRepository = new Repository<AccountRole>(context);
                }
                return accountRoleRepository;
            }
        }
        public Repository<Role> RoleRepository
        {
            get
            {
                if (this.roleRepository == null)
                {
                    this.roleRepository = new Repository<Role>(context);
                }
                return roleRepository;
            }
        }
        public Repository<UserAccount> UserAccountRepository
        {
            get
            {
                if (this.userAccountRepository == null)
                {
                    this.userAccountRepository = new Repository<UserAccount>(context);
                }
                return userAccountRepository;
            }
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Save changes in the context
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
