using Ioon.Domain.Common.Interfaces.Base;
using Ioon.Domain.Primitives;
using Ioon.Domain.ValueObjects;

namespace Ioon.Domain
{
    public partial class Category : AggregateRoot, IEntity
    {
        public Category(Guid businessId, Name categoryName)
        {
            BusinessId = businessId;
            CategoryName = categoryName;
        }

        public Guid BusinessId { get; private set; }

        public Name CategoryName { get; private set; }
    }
}
