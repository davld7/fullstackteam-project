using System;
using System.Collections.Generic;

namespace Ioon.Domain;

public partial class PaymentType
{
    public int PaymentId { get; set; }

    public string PaymentName { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
