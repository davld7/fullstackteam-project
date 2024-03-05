using System;
using System.Collections.Generic;

namespace Ioon.Domain;

public partial class OwnersBusiness
{
    public int OwnerId { get; set; }

    public Guid OwnerUuid { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Identification { get; set; } = null!;

    public byte[] PasswordHashed { get; set; } = null!;

    public byte[] PasswordSalt { get; set; } = null!;

    public int RoleId { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public DateTime? LastPasswordChangedDate { get; set; }

    public DateTime? LastActivityDate { get; set; }

    public virtual ICollection<Business> Businesses { get; set; } = new List<Business>();

    public virtual Role Role { get; set; } = null!;
}
