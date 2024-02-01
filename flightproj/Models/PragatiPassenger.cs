using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace flightproj.Models;

public partial class PragatiPassenger
{
    [Display(Name = "Ticket Number")]
    public int Passengerid { get; set; }

    public string? Username { get; set; }

    [Required]
    [Display(Name = "Passenger Name")]
    public string? Name { get; set; }

    [Display(Name = "Contact Number")]
    [Required]
    [StringLength(maximumLength:10,MinimumLength =10,ErrorMessage ="Contact Number should be exactly 10 digits")]
    public string? ContactNo { get; set; }

    [Display(Name = "Flight ID")]
    public int? Flightid { get; set; }

    public virtual PragatiFlight? Flight { get; set; }

    public virtual ICollection<PragatiBooking> PragatiBookings { get; set; } = new List<PragatiBooking>();

    public virtual PragatiFlightUser? UsernameNavigation { get; set; }
}
