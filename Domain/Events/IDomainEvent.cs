namespace Domain.Events;

public interface IDomainEvent : INotification // اگر از MediatR استفاده می‌کنی
{
    DateTime OccurredOn { get; }
}
