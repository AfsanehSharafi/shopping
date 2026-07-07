namespace Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; private set; }

        // --- بخش جدید برای مدیریت Domain Events ---
        private readonly List<IDomainEvent> _domainEvents = new();

        // دسترسی فقط خواندنی به رویدادها برای لایه‌های دیگر
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        // متدی برای اضافه کردن رویداد (فقط برای کلاس‌های فرزند مثل Order)
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        // متدی برای پاک کردن رویدادها بعد از انتشار
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
        // ------------------------------------------

        protected void SetUpdatedAt()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
