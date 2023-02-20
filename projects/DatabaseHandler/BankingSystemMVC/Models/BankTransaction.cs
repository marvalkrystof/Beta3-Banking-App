using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankingSystemMVC.Models;

public partial class BankTransaction
{
    public int Id { get; set; }

    [Display(Name = "From account")]
    public int FromAccountId { get; set; }

    [Display(Name = "To account")]
    public int ToAccountId { get; set; }

    [Required(ErrorMessage = "You have to enter the sending amount")]
    [Display(Name = "Amount")]
    [Range (0.01,9999999.99, ErrorMessage = "Minimum value 0.01 and maximum value is 9999999.99")]
    public decimal Amount { get; set; }

    public DateTime? TransactionDate { get; set; }

    [Display(Name = "Note")]
    [StringLength(100, ErrorMessage = "The note has to be maximum 100 characters")]
    public string? Note { get; set; }

    public virtual Account? FromAccount { get; set; } = null!;

    public virtual Account? ToAccount { get; set; } = null!;
}
