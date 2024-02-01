using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flightproj.Models;

public partial class PragatiFlightUser
{
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    [DataType(DataType.EmailAddress,ErrorMessage = "Enter a valid email address")]
    public string Email { get; set; } = null!;

    [Required]
    public string? Password { get; set; }

    [NotMapped]
    [Required]
    [Compare("Password",ErrorMessage = "Passwords do not match")]
    public string? ConfirmPassword { get; set; }

    public virtual ICollection<PragatiPassenger> PragatiPassengers { get; set; } = new List<PragatiPassenger>();
}
