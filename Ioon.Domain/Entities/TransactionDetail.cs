namespace Ioon.Domain;

public partial class TransactionDetail
{
    public int TransactionDetailsId { get; set; }

    public Guid TransactionUuid { get; set; }

    public string ProductName { get; set; } = null!;

    public int Quantity { get; set; }

    public double Price { get; set; }

    public double? Discount { get; set; }

    public double? Ivamount { get; set; }

    public virtual Transaction TransactionUu { get; set; } = null!;
}
