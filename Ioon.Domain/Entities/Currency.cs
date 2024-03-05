using System;
using System.Collections.Generic;

namespace Ioon.Domain;

public partial class Currency
{
    public int CurrencyId { get; set; }

    public string CurrencyName { get; set; } = null!;

    public string CurrencyCode { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
