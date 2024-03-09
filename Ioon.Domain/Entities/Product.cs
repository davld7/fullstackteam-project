using Ioon.Domain.Common.Interfaces.Base;
using Ioon.Domain.ValueObjects;

namespace Ioon.Domain;

public partial class Product : IEntity
{
    public Product(Guid productUuid, Guid businessUuid, Name productName, Money price, int stock, decimal? discount, Guid categoryId, string imgUrl, string description)
    {
        ProductUUID = productUuid;
        BusinessUUID = businessUuid;
        ProductName = productName;
        Price = price;
        Stock = stock;
        Discount = discount;
        CategoryUUID = categoryId;
        ImgUrl = imgUrl;
        Description = description;
    }

    public Guid ProductUUID { get; private set; }

    public Guid BusinessUUID { get; private set; }

    public Name ProductName { get; private set; }

    public Money Price { get; private set; }

    public int Stock { get; private set; }

    public decimal? Discount { get; private set; }

    public Guid CategoryUUID { get; private set; }

    public string ImgUrl { get; private set; }

    public string Description { get; private set; }

}
