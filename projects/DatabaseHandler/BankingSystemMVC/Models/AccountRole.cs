﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankingSystemMVC.Models;

public partial class AccountRole
{
    public int Id 
    { 
        get; 
        set; 
    }

    [Display(Name = "Role")]
    public int RoleId 
    { 
        get; 
        set;
    }

    [Display(Name = "User account")]
    public int UserAccountId 
    { 
        get; 
        set;
    }

    public virtual Role Role 
    { 
        get; 
        set; 
    } = null!;

    public virtual UserAccount UserAccount 
    { 
        get; 
        set; 
    } = null!;
}
