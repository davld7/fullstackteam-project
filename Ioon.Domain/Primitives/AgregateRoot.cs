namespace Ioon.Domain.Primitives
{
    public abstract class AgregateRoot
    {
        private readonly List<DomainEvent> _domainEvents = new();

        public IReadOnlyCollection<DomainEvent> GetDomainEvents() => _domainEvents;

        protected void RaiseDomainEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    }
}
