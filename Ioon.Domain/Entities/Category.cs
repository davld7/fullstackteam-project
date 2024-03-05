using System;
using System.Collections.Generic;

namespace Ioon.Domain;

public partial class Category
{
    public int CategoryId { get; set; }

    public int BusinessId { get; set; }

    public string CategoryName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public virtual Business Business { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
