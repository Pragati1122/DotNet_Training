using System;
using System.Collections.Generic;

namespace flightapi.Models;

public partial class PragatiBooking
{
    public int Bookingid { get; set; }

    public int? Flightid { get; set; }

    public int? Passengerid { get; set; }

    public DateTime? Bookingdate { get; set; }

    public decimal? TotalCost { get; set; }

    public virtual PragatiFlight? Flight { get; set; }

    public virtual PragatiPassenger? Passenger { get; set; }
}
