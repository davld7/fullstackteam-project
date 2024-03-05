using System;
using System.Collections.Generic;

namespace Ioon.Domain;

public partial class BusinessType
{
    public int BusinessTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Business> Businesses { get; set; } = new List<Business>();
}
