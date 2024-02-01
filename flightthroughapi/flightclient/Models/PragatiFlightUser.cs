using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace flightapi.Models;

public partial class PragatiFlightUser
{
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string? Password { get; set; }

    // public virtual ICollection<PragatiPassenger> PragatiPassengers { get; set; } = new List<PragatiPassenger>();
}
