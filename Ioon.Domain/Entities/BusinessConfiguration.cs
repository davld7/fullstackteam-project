using System;
using System.Collections.Generic;

namespace Ioon.Domain;

public partial class BusinessConfiguration
{
    public int ConfigId { get; set; }

    public int BusinessId { get; set; }

    public bool IsActiveIva { get; set; }

    public decimal Iva { get; set; }

    public bool IsActivePayCash { get; set; }

    public bool IsActiveCardPayment { get; set; }

    public decimal Usdexchange { get; set; }

    public string? TokenCommerce { get; set; }

    public string? ServiceCommerce { get; set; }

    public virtual Business Business { get; set; } = null!;
}
