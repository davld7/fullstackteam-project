namespace Ioon.Domain;

public partial class Product
{

    public Guid ProductUuid { get; set; }

    public int BusinessUuid { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public decimal? Discount { get; set; }

    public int CategoryUuid { get; set; }

    public string ImgUrl { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool? IsActive { get; set; }

}
