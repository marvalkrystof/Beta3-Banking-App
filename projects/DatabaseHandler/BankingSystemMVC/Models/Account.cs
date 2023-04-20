using System;
using System.Collections.Generic;

namespace BankingSystemMVC.Models;

/// <summary>
/// Class representing the bank Account table
/// </summary>
public partial class Account
{
    public int Id 
    { 
        get; 
        set;
    }

    public int? SavingsAccountTypeId 
    { 
        get; 
        set; 
    }

    public int? PersonalAccountTypeId 
    { 
        get; 
        set;
    }

    public int CustomerId 
    { 
        get; 
        set;
    }

    public int CurrencyId 
    { 
        get; 
        set;
    }

    public int AccountNumber 
    { 
        get; 
        set;
    }

    public decimal Balance 
    { 
        get; 
        set;
    }

    public virtual ICollection<BankTransaction> BankTransactionFromAccounts 
    { 
        get;
    } = new List<BankTransaction>();

    public virtual ICollection<BankTransaction> BankTransactionToAccounts 
    { 
        get;
    } = new List<BankTransaction>();

    public virtual Currency Currency 
    { 
        get;
        set;
    } = null!;

    public virtual Customer Customer 
    { 
        get; 
        set; 
    } = null!;

    public virtual PersonalAccountType? PersonalAccountType 
    { 
        get; 
        set;
    }

    public virtual SavingsAccountType? SavingsAccountType 
    { 
        get; 
        set; 
    }
}
