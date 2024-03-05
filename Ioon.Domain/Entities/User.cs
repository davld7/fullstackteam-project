using Ioon.Domain.Primitives;
using Ioon.Domain.Primitives.Entities;
using Ioon.Domain.ValueObjects;

namespace Ioon.Domain;

public sealed class User : AgregateRoot
{
    public User()
    {
    }

    public User(UserId userId, string businessId, UserName userName, EmailAddress email, byte[] passwordHashed, byte[] passwordSalt, PhoneNumber phoneNumber, Identification identification, string roleId)
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

    public UserId UserId { get; private set; }

    public string BusinessId { get; private set; }

    public UserName UserName { get; private set; }

    public EmailAddress Email { get; private set; }

    public byte[] PasswordHashed { get; private set; }

    public byte[] PasswordSalt { get; private set; }

    public PhoneNumber PhoneNumber { get; private set; }

    public Identification Identification { get; private set; }

    public string RoleId { get; private set; }

}
