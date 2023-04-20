using System;
using System.Collections.Generic;

namespace BankingSystemMVC.Models;

/// <summary>
/// Model representing the Card table in the DB, used for Credit card information tied to the bank Accounts
/// </summary>
public partial class Card
{
    public int Id 
    { 
        get; 
        set;
    }

    public int AccountId 
    { 
        get; 
        set; 
    }

    public int CardBrandId 
    { 
        get; 
        set; 
    }

    public decimal CardNumber 
    { 
        get; 
        set;
    }

    public DateTime IssueDate 
    { 
        get; 
        set;
    }

    public DateTime ExpireDate 
    { 
        get; 
        set; 
    }

    public bool IsDebit 
    { 
        get; 
        set; 
    }

    public virtual CardBrand CardBrand 
    { 
        get; 
        set;
    } = null!;
}
