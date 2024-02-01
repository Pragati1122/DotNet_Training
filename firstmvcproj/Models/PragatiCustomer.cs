using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace firstmvcproj.Models;

public partial class PragatiCustomer
{
    public int Cid { get; set; }

    [Required]
    [Display(Name = "Customer Name")]
    public string? Cname { get; set; }

    [Range(minimum:5000,maximum:100000,ErrorMessage = "Salary must be between 5K and 1L")]
    public int? Salary { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Date Of Joining")]
    public DateOnly? Doj { get; set; }

    [Display(Name = "Contact Number")]
    public string? Phno { get; set; }

    [DataType(DataType.EmailAddress,ErrorMessage = "Enter a valid email address")]
    public string? Email { get; set; }

    public string? Password { get; set; }

    [NotMapped]
    [Compare("Password",ErrorMessage = "Passwords do not match")]
    public string? ConfirmPassword { get; set; }
}
