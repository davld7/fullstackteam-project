using MediatR;

namespace Ioon.Domain.Primitives
{
    public record DomainEvent(Guid Id, DateTime OccurredOn) : INotification;
}
