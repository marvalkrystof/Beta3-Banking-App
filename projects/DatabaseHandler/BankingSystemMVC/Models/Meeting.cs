using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankingSystemMVC.Models;

public partial class Meeting
{
    public int Id { get; set; }

    [Display(Name = "Employee")]
    [Required]
    public int EmployeeId { get; set; }

    [Display(Name = "Customer")]
    [Required]
    public int CustomerId { get; set; }

    [Display(Name = "Title")]
    [Required]
    public string ShortDescription { get; set; } = null!;

    [Display(Name = "Text")]
    [Required]
    public string Text { get; set; } = null!;


    public DateTime? RequestCreatedDate { get; set; }
    [Display (Name = "Meeting date")]
    [DataType(DataType.Date)]
    public DateTime? MeetingDate { get; set; }

    public virtual Customer? Customer { get; set; } = null!;

    public virtual Employee? Employee { get; set; } = null!;
}
