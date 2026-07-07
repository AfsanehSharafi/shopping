using Domain.Common;

namespace Domain.Events;

/// <summary>
/// این کلاس نشان‌دهنده اتفاقی است که وقتی یک سفارش جدید ساخته می‌شود، رخ می‌دهد.
/// </summary>
public class OrderCreatedEvent : IDomainEvent
{
    // اطلاعاتی که برای رویداد نیاز داریم (مثلاً آی‌دی سفارش)
    public Guid OrderId { get; }
    public DateTime OccurredOn { get; }

    public OrderCreatedEvent(Guid orderId)
    {
        OrderId = orderId;
        // ثبت زمان دقیق وقوع رویداد
        OccurredOn = DateTime.UtcNow;
    }


}
