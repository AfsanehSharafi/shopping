namespace Domain.Common;

/// <summary>
/// این اینترفیس پایه برای تمام رویدادهای دامین ما خواهد بود.
/// هر اتفاقی که در دامین بیفتد، باید این اینترفیس را پیاده‌سازی کند.
/// </summary>
public interface IDomainEvent
{
    // معمولاً رویدادها زمانی که رخ می‌دهند، تاریخ و زمان را هم با خود دارند
    DateTime OccurredOn { get; }
}
