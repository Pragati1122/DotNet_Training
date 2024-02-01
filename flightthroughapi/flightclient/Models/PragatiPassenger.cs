using System;
using System.Collections.Generic;

namespace flightapi.Models;

public partial class PragatiPassenger
{
    public int Passengerid { get; set; }

    public string? Username { get; set; }

    public string? Name { get; set; }

    public string? ContactNo { get; set; }

    public int? Flightid { get; set; }

    // public virtual PragatiFlight? Flight { get; set; }

    // public virtual ICollection<PragatiBooking> PragatiBookings { get; set; } = new List<PragatiBooking>();

    // public virtual PragatiFlightUser? UsernameNavigation { get; set; }
}
