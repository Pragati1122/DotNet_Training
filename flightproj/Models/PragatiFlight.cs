using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace flightproj.Models;

public partial class PragatiFlight
{
    public int Flightid { get; set; }

    public string? Flightname { get; set; }

    public string? Source { get; set; }

    public string? Destination { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public string? Layover { get; set; }

    public int? SeatsAvailable { get; set; }

    public decimal? Price { get; set; }

    [DataType(DataType.Date)]
    public DateOnly? Date { get; set; }

    public virtual ICollection<PragatiBooking> PragatiBookings { get; set; } = new List<PragatiBooking>();

    public virtual ICollection<PragatiPassenger> PragatiPassengers { get; set; } = new List<PragatiPassenger>();
}
