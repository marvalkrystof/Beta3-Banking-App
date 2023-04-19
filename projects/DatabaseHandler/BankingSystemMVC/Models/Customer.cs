using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankingSystemMVC.Models;

public partial class Customer
{
    public int Id 
    { 
        get; 
        set; 
    }

    [Display(Name = "First name")]
    [Required]
    [StringLength(50, ErrorMessage = "maximum 50 characters")]
    public string FirstName 
    { 
        get; 
        set; 
    } = null!;

    [Display(Name = "Last name")]
    [Required]
    [StringLength(50, ErrorMessage = "maximum 50 characters")]
    public string LastName 
    { 
        get; 
        set;
    } = null!;

    [Display(Name = "Phone number")]
    [Required]
    [Range(100000000, 999999999, ErrorMessage = "Value must be between 100000000 to 999999999")]
    public int PhoneNumber 
    { 
        get; 
        set;
    }

    [Display(Name = "E-mail")]
    [Required]
    [StringLength(70, ErrorMessage = "maximum 70 characters")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]

    public string EmailAddress 
    { 
        get; 
        set; 
    } = null!;

    [Display(Name = "Street address")]
    [StringLength(60, ErrorMessage = "maximum 60 characters")]
    [Required]
    public string AddressStreet 
    { 
        get; 
        set;
    } = null!;

    [Display(Name = "Street number")]
    [Required]
    public int AddressStreetNumber 
    { 
        get; 
        set;
    }


    [Display(Name = "City")]
    [StringLength(60, ErrorMessage = "maximum 60 characters")]
    [Required]
    public string AddressCity 
    { 
        get; 
        set;
    } = null!;
    [Display(Name = "ZIP")]
    [Required]
    [Range(10000, 99999, ErrorMessage = "Value must be between 10000 to 99999")]
    public int AddressZip 
    { 
        get; 
        set;
    }

    public virtual ICollection<Account>? Accounts 
    { 
        get;
    } = new List<Account>();

    public virtual ICollection<Meeting>? Meetings 
    { 
        get;
    } = new List<Meeting>();

    public virtual ICollection<UserAccount>? UserAccounts 
    { 
        get;
    } = new List<UserAccount>();

}
