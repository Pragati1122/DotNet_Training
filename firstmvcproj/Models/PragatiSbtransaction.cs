using System;
using System.Collections.Generic;

namespace firstmvcproj.Models;

public partial class PragatiSbtransaction
{
    public int TransactionId { get; set; }

    public DateTime? TransactionDate { get; set; }

    public decimal? Amount { get; set; }

    public string? TransactionType { get; set; }

    public int? AccountNumber { get; set; }

    public virtual PragatiSbaccount? AccountNumberNavigation { get; set; }
}
