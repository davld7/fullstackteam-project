using Ioon.Application.Common.Interfaces.Data;
using Ioon.Domain;
using Ioon.Domain.Common.Interfaces.Base;
using Ioon.Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ioon.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
    {
        private readonly IPublisher _publisher;

        public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base(options)
        {
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Business> Businesses { get; set; }
 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var domainEvents = ChangeTracker.Entries<AggregateRoot>()
                .Select(x => x.Entity)
                .Where(x => x.GetDomainEvents().Any())
                .SelectMany(x => x.GetDomainEvents())
                .ToList();

            var result = await base.SaveChangesAsync(cancellationToken);

            await Task.WhenAll(domainEvents.Select(e => _publisher.Publish(e, cancellationToken)));

            return result;
        }
    }
}
