using Ioon.Domain.Common.Interfaces.Base;

namespace Ioon.Application.Common.DTO
{
    [Serializable]
    public class ProductDTO : IModelResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public decimal Discount { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string ImgUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
