using System;
using System.Collections.Generic;

namespace BankingSystemMVC.Models;

public partial class Card
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public int CardBrandId { get; set; }

    public decimal CardNumber { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime ExpireDate { get; set; }

    public bool IsDebit { get; set; }

    public virtual CardBrand CardBrand { get; set; } = null!;
}
