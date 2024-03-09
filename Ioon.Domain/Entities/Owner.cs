using Ioon.Domain.Common.Interfaces.Base;
using Ioon.Domain.ValueObjects;

namespace Ioon.Domain;

public partial class Owner : IEntity
{
    public Owner(Guid ownerId, Name fullName, EmailAddress email, PhoneNumber phone, Identification identification, byte[] passwordHashed, byte[] passwordSalt, Guid roleId)
    {
        OwnerId = ownerId;
        FullName = fullName;
        Email = email;
        Phone = phone;
        Identification = identification;
        PasswordHashed = passwordHashed;
        PasswordSalt = passwordSalt;
        RoleId = roleId;
    }

    public Guid OwnerId { get; private set; }

    public Name FullName { get; private set; }

    public EmailAddress Email { get; private set; }

    public PhoneNumber Phone { get; private set; }

    public Identification Identification { get; private set; }

    public byte[] PasswordHashed { get; private set; }

    public byte[] PasswordSalt { get; private set; }

    public Guid RoleId { get; private set; }

}
