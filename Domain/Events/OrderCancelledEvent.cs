using Domain.Events;

namespace Domain.Events;

public record OrderCancelledEvent(Guid OrderId) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
