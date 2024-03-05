using Ioon.Domain;
using Microsoft.EntityFrameworkCore;

namespace Ioon.Application.Common.Interfaces.Data
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<OwnersBusiness> Owners { get; set; }
        DbSet<Business> Businesses { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
