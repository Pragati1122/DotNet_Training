using System;
using System.Collections.Generic;

namespace flightapi.Models;

public partial class PragatiFlightUser
{
    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    public virtual ICollection<PragatiPassenger> PragatiPassengers { get; set; } = new List<PragatiPassenger>();
}
