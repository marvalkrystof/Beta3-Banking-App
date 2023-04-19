using System;
using System.Collections.Generic;

namespace BankingSystemMVC.Models;

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
