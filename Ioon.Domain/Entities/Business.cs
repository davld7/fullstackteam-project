using Ioon.Domain.Common.Interfaces.Base;
using Ioon.Domain.ValueObjects;

namespace Ioon.Domain;

public partial class Business : IEntity
{

    public Business(Guid businessId, Guid ownerId, Name businessName, EmailAddress email, PhoneNumber phone, string? address, string imgUrl, Ruc ruc, Guid departmentId, Guid businessTypeId)
    {
        BusinessUUID = businessId;
        OwnerUUID = ownerId;
        BusinessName = businessName;
        Email = email;
        Phone = phone;
        Address = address;
        ImgUrl = imgUrl;
        Ruc = ruc;
        DepartmentId = departmentId;
        BusinessTypeId = businessTypeId;
    }

    public Guid BusinessUUID { get; private set; }

    public Guid OwnerUUID { get; private set; }

    public Name BusinessName { get; private set; }

    public EmailAddress Email { get; private set; }

    public PhoneNumber Phone { get; private set; }

    public string? Address { get; private set; }

    public string ImgUrl { get; private set; }

    public Ruc Ruc { get; private set; }

    public Guid DepartmentId { get; private set; }

    public Guid BusinessTypeId { get; private set; }

}
