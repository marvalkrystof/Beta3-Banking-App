using System;
using System.Collections.Generic;
using BankingSystemMVC.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace BankingSystemMVC.Models;

public partial class BankingSystemDbContext : DbContext
{
    public BankingSystemDbContext()
    {
        IsolationLevel.SetIsolationLevelContext(this, System.Data.IsolationLevel.ReadUncommitted);
    }

    public BankingSystemDbContext(DbContextOptions<BankingSystemDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<BankTransaction> BankTransactions { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<CardBrand> CardBrands { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Meeting> Meetings { get; set; }

    public virtual DbSet<PersonalAccountType> PersonalAccountTypes { get; set; }

    public virtual DbSet<SavingsAccountType> SavingsAccountTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder
        .LogTo(Logger.Logger.Log)
        .UseLazyLoadingProxies()
        .UseSqlServer("Server=(localdb)\\Local;Database=Banking_System;User Id=Banking_App;Password=ahoj123;");
         //.UseSqlServer(CreateConnectionString());


    private string CreateConnectionString()
    {
        string server = System.Configuration.ConfigurationManager.AppSettings["server"];
        if (server == null)
        {
            Logger.Logger.LogCriticalFailure("Server parameter in web config is null");
        }

        string database = System.Configuration.ConfigurationManager.AppSettings["database"];
        if (database == null)
        {
            Logger.Logger.LogCriticalFailure("Database parameter in web config is null");
        }

        string username = System.Configuration.ConfigurationManager.AppSettings["username"];
        if (username == null)
        {
            Logger.Logger.LogCriticalFailure("Username parameter in web config is null");
        }


        string password = System.Configuration.ConfigurationManager.AppSettings["password"];
        if (password == null)
        {
            Logger.Logger.LogCriticalFailure("Password parameter in web config is null");
        }

        return String.Format("Server={0};Database={1};User Id={2};Password={3};",server,database,username,password);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("db_owner");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3213E83F35B3542F");

            entity.ToTable("Account", "dbo");

            entity.HasIndex(e => e.AccountNumber, "UQ__Account__AF91A6ADB4893D20").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountNumber).HasColumnName("account_number");
            entity.Property(e => e.Balance)
                .HasColumnType("numeric(28, 2)")
                .HasColumnName("balance");
            entity.Property(e => e.CurrencyId).HasColumnName("Currency_id");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_id");
            entity.Property(e => e.PersonalAccountTypeId).HasColumnName("Personal_Account_Type_id");
            entity.Property(e => e.SavingsAccountTypeId).HasColumnName("Savings_Account_Type_id");

            entity.HasOne(d => d.Currency).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.CurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Account__Currenc__0C85DE4D");

            entity.HasOne(d => d.Customer).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Account__Custome__0B91BA14");

            entity.HasOne(d => d.PersonalAccountType).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.PersonalAccountTypeId)
                .HasConstraintName("FK__Account__Persona__0A9D95DB");

            entity.HasOne(d => d.SavingsAccountType).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.SavingsAccountTypeId)
                .HasConstraintName("FK__Account__Savings__09A971A2");
        });

        modelBuilder.Entity<BankTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bank_Tra__3213E83FC44D5609");

            entity.ToTable("Bank_Transaction", "dbo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("numeric(28, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.FromAccountId).HasColumnName("from_Account_id");
            entity.Property(e => e.Note)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("note");
            entity.Property(e => e.ToAccountId).HasColumnName("to_Account_id");
            entity.Property(e => e.TransactionDate)
                .HasColumnType("date")
                .HasColumnName("transaction_date");

            entity.HasOne(d => d.FromAccount).WithMany(p => p.BankTransactionFromAccounts)
                .HasForeignKey(d => d.FromAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bank_Tran__from___0F624AF8");

            entity.HasOne(d => d.ToAccount).WithMany(p => p.BankTransactionToAccounts)
                .HasForeignKey(d => d.ToAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bank_Tran__to_Ac__10566F31");
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Card__3213E83F0FEDD804");

            entity.ToTable("Card", "dbo");

            entity.HasIndex(e => e.CardNumber, "UQ__Card__1E6E0AF490223462").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("Account_id");
            entity.Property(e => e.CardBrandId).HasColumnName("Card_Brand_id");
            entity.Property(e => e.CardNumber)
                .HasColumnType("numeric(16, 0)")
                .HasColumnName("card_number");
            entity.Property(e => e.ExpireDate)
                .HasColumnType("date")
                .HasColumnName("expire_date");
            entity.Property(e => e.IsDebit).HasColumnName("isDebit");
            entity.Property(e => e.IssueDate)
                .HasColumnType("date")
                .HasColumnName("issue_date");

            entity.HasOne(d => d.CardBrand).WithMany(p => p.Cards)
                .HasForeignKey(d => d.CardBrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Card__Card_Brand__787EE5A0");
        });

        modelBuilder.Entity<CardBrand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Card_Bra__3213E83F306D7055");

            entity.ToTable("Card_Brand", "dbo");

            entity.HasIndex(e => e.Name, "UQ__Card_Bra__72E12F1B111BF00B").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Currency__3213E83F05D100AF");

            entity.ToTable("Currency", "dbo");

            entity.HasIndex(e => e.Name, "UQ__Currency__72E12F1B12970B75").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Sign)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("sign");
            entity.Property(e => e.UsdConversionRate)
                .HasColumnType("decimal(28, 3)")
                .HasColumnName("usd_conversion_rate");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3213E83FB6A9054A");

            entity.ToTable("Customer", "dbo");

            entity.HasIndex(e => e.EmailAddress, "UQ__Customer__20C6DFF5DFDA025E").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "UQ__Customer__A1936A6B3EF22A1C").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddressCity)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("address_city");
            entity.Property(e => e.AddressStreet)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("address_street");
            entity.Property(e => e.AddressStreetNumber).HasColumnName("address_street_number");
            entity.Property(e => e.AddressZip).HasColumnName("address_zip");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("email_address");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasColumnType("numeric(9, 0)")
                .HasColumnName("phone_number");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3213E83F939DC5AA");

            entity.ToTable("Employee", "dbo");

            entity.HasIndex(e => e.EmailAddress, "UQ__Employee__20C6DFF5ECE43B8E").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "UQ__Employee__A1936A6B3EA79D34").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddressCity)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("address_city");
            entity.Property(e => e.AddressStreet)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("address_street");
            entity.Property(e => e.AddressStreetNumber).HasColumnName("address_street_number");
            entity.Property(e => e.AddressZip).HasColumnName("address_zip");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("email_address");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasColumnType("numeric(9, 0)")
                .HasColumnName("phone_number");
        });

        modelBuilder.Entity<Meeting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Meeting__3213E83F97C075DF");

            entity.ToTable("Meeting", "dbo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_id");
            entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");
            entity.Property(e => e.MeetingDate)
                .HasColumnType("date")
                .HasColumnName("meeting_date");
            entity.Property(e => e.RequestCreatedDate)
                .HasColumnType("date")
                .HasColumnName("request_created_date");
            entity.Property(e => e.ShortDescription)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("short_description");
            entity.Property(e => e.Text)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("text");

            entity.HasOne(d => d.Customer).WithMany(p => p.Meetings)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Meeting__Custome__01142BA1");

            entity.HasOne(d => d.Employee).WithMany(p => p.Meetings)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Meeting__Employe__00200768");
        });
        modelBuilder.Entity<PersonalAccountType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Personal__3213E83F9954654C");

            entity.ToTable("Personal_Account_Type", "dbo");

            entity.HasIndex(e => e.TypeName, "UQ__Personal__543C4FD9976E84C9").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MaintenanceFee)
                .HasColumnType("numeric(28, 2)")
                .HasColumnName("maintenance_fee");
            entity.Property(e => e.TypeName)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("type_name");
        });
        modelBuilder.Entity<SavingsAccountType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Savings___3213E83FF86B190B");

            entity.ToTable("Savings_Account_Type", "dbo");

            entity.HasIndex(e => e.TypeName, "UQ__Savings___543C4FD90746F62E").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.InterestRate)
                .HasColumnType("numeric(28, 2)")
                .HasColumnName("interest_rate");
            entity.Property(e => e.TypeName)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("type_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
