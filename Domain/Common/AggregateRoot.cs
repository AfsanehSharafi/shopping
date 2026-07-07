using Domain.Events;

namespace Domain.Common
{
    public abstract class AggregateRoot : BaseEntity
    {
        //private readonly List<object> _domainEvents = new();
        //public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();


        //protected void AddDomainEvent(object domainEvent)
        //{
        //    _domainEvents.Add(domainEvent);
        //}

        //public void ClearDomainEvents()
        //{
        //    _domainEvents.Clear();
        //}

        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(IDomainEvent eventItem)
            => _domainEvents.Add(eventItem);

        public void ClearDomainEvents()
            => _domainEvents.Clear();
    }
}
