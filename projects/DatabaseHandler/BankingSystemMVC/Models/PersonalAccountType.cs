using System;
using System.Collections.Generic;

namespace BankingSystemMVC.Models;

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
