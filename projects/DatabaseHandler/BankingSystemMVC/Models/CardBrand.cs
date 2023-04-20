using System;
using System.Collections.Generic;

namespace BankingSystemMVC.Models;

/// <summary>
/// Model representing the Card Brand in the DB
/// </summary>
public partial class CardBrand
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

    public virtual ICollection<Card> Cards 
    { 
        get;
    } = new List<Card>();
}
