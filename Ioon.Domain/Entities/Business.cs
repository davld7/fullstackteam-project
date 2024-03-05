using Ioon.Domain.Primitives.Entities;
using Ioon.Domain.ValueObjects;

namespace Ioon.Domain;

public partial class Business
{

    public Business()
    {

    }
    public Business(BusinessId businessId, Guid ownerId, BusinessName businessName, EmailAddress email, PhoneNumber phone, string? address, string imgUrl, Ruc ruc, Guid departmentId, Guid businessTypeId)
    {
        BusinessId = businessId;
        OwnerId = ownerId;
        BusinessName = businessName;
        Email = email;
        Phone = phone;
        Address = address;
        ImgUrl = imgUrl;
        Ruc = ruc;
        DepartmentId = departmentId;
        BusinessTypeId = businessTypeId;
    }

    public BusinessId BusinessId { get; private set; }

    public Guid OwnerId { get; private set; }

    public BusinessName BusinessName { get; private set; }

    public EmailAddress Email { get; private set; }

    public PhoneNumber Phone { get; private set; }

    public string? Address { get; private set; }

    public string ImgUrl { get; private set; }

    public Ruc Ruc { get; private set; }

    public Guid DepartmentId { get; private set; }

    public Guid BusinessTypeId { get; private set; }

}
