using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankingSystemMVC.Models;

/// <summary>
/// Model representing the UserAccount - (webapp) account from the db.
/// </summary>
public partial class UserAccount
{
    public int Id 
    { 
        get; 
        set; 
    }

    [Display(Name = "Employee")]
    public int? EmployeeId 
    { 
        get; 
        set; 
    }

    [Display(Name = "Customer")]
    public int? CustomerId 
    { 
        get; 
        set;
    }

    [Display(Name = "Username")]    
    [Required]
    [StringLength(60, ErrorMessage = "maximum 60 characters")]
    public string Username 
    { 
        get; 
        set;
    } = null!;

    [Display(Name = "Password")]
    [Required]
    public string Password 
    { 
        get; 
        set;
    } = null!;

    public virtual ICollection<AccountRole> AccountRoles 
    { 
        get;
    } = new List<AccountRole>();

    public virtual Customer? Customer 
    { 
        get; 
        set;
    }

    public virtual Employee? Employee 
    { 
        get; 
        set;
    }
}
