using Domain.Common;

namespace Domain.Entities
{
    public enum OrderStatus
    {
        Pending,    // در انتظار پرداخت
        Paid,       // پرداخت شده
        Shipped,    // ارسال شده
        Delivered,  // تحویل شده
        Cancelled   // لغو شده
    }

    public class Order : AggregateRoot
    {
        public DateTime OrderDate { get; private set; }
        public decimal TotalAmount { get; private set; }
        public OrderStatus Status { get; private set; }


        public Guid UserId { get; private set; }
        public User User { get; private set; }

        // Navigation Property: لیست آیتم‌های این سفارش
        public ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();

        // Private constructor for EF Core
        private Order() { }
    }
}
