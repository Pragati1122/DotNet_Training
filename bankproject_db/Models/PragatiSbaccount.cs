using System;
using System.Collections.Generic;

namespace bankproject.Models;

public partial class PragatiSbaccount
{
    public int AccountNumber { get; set; }

    public string? CustomerName { get; set; }

    public string? CustomerAddress { get; set; }

    public decimal? CurrentBalance { get; set; }

    public virtual ICollection<PragatiSbtransaction> PragatiSbtransactions { get; set; } = new List<PragatiSbtransaction>();
}
