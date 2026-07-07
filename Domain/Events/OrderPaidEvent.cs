using Domain.Events;

namespace Domain.Events;

public record OrderPaidEvent(Guid OrderId) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
