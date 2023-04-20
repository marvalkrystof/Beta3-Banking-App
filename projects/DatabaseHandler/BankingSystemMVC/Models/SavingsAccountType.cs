using System;
using System.Collections.Generic;

namespace BankingSystemMVC.Models;

/// <summary>
/// Model representing the Savings Account Type from the DB.
/// </summary>
public partial class SavingsAccountType
{
    public int Id 
    { 
        get; 
        set;
    }

    public string TypeName 
    { 
        get; 
        set;
    } = null!;

    public decimal InterestRate 
    { 
        get; 
        set;
    }

    public virtual ICollection<Account> Accounts 
    { 
        get;
    } = new List<Account>();
}
