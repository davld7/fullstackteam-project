using Ioon.Domain.Common.Interfaces.Base;
using Ioon.Domain.ValueObjects;

namespace Ioon.Domain;

public sealed class User : IEntity
{
    public User(Guid userId, Guid businessId, Name userName, EmailAddress email, byte[] passwordHashed, byte[] passwordSalt, PhoneNumber phoneNumber, Identification identification, Guid roleId)
    {
        UserId = userId;
        BusinessId = businessId;
        UserName = userName;
        Email = email;
        PasswordHashed = passwordHashed;
        PasswordSalt = passwordSalt;
        PhoneNumber = phoneNumber;
        Identification = identification;
        RoleId = roleId;
    }

    public Guid UserId { get; private set; }

    public Guid BusinessId { get; private set; }

    public Name UserName { get; private set; }

    public EmailAddress Email { get; private set; }

    public byte[] PasswordHashed { get; private set; }

    public byte[] PasswordSalt { get; private set; }

    public PhoneNumber PhoneNumber { get; private set; }

    public Identification Identification { get; private set; }

    public Guid RoleId { get; private set; }

}
