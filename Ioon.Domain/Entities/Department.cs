using System;
using System.Collections.Generic;

namespace Ioon.Domain;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public virtual ICollection<Business> Businesses { get; set; } = new List<Business>();
}
