using Ioon.Application.Common.DTO;
using Ioon.Domain;

namespace Ioon.Application.Common.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product, ProductDTO>
    {   
    }
}
