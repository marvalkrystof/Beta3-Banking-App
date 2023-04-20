using System;
using System.Collections.Generic;

namespace BankingSystemMVC.Models;

/// <summary>
/// Model representing the Personal Account Type from the DB.
/// </summary>
public partial class PersonalAccountType
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

    public decimal MaintenanceFee 
    { 
        get; 
        set;
    }

    public virtual ICollection<Account> Accounts 
    { 
        get;
    } = new List<Account>();
}
