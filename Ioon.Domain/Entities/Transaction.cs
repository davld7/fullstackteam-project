using System;
using System.Collections.Generic;

namespace Ioon.Domain;

public partial class Transaction
{
    public Guid TransactionUuid { get; set; }

    public int BusinessId { get; set; }

    public string Ntrasaction { get; set; } = null!;

    public decimal SubTotal { get; set; }

    public decimal Iva { get; set; }

    public decimal AmountTotal { get; set; }

    public int PaymentTypeId { get; set; }

    public int CurrencyId { get; set; }

    public DateTime TransactionDate { get; set; }

    public virtual Business Business { get; set; } = null!;

}
