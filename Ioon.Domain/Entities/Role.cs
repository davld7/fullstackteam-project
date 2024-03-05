using System;
using System.Collections.Generic;

namespace Ioon.Domain;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<OwnersBusiness> OwnersBusinesses { get; set; } = new List<OwnersBusiness>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
