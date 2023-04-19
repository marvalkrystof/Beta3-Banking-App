using System;
using System.Collections.Generic;

namespace BankingSystemMVC.Models;

public partial class Currency
{
    public int Id 
    { 
        get; 
        set;
    }

    public string Name 
    { 
        get; 
        set;
    } = null!;

    public string Sign 
    { 
        get; 
        set;
    } = null!;

    public decimal UsdConversionRate 
    { 
        get; 
        set;
    }

    public virtual ICollection<Account> Accounts 
    { 
        get;
    } = new List<Account>();
}
