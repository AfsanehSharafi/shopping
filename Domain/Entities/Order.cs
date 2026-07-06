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

        public Order(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            OrderDate = DateTime.UtcNow;
            Status = OrderStatus.Pending;
            TotalAmount = 0;
        }

        // متد برای اضافه کردن آیتم به سفارش
        public void AddOrderItem(Product product, int quantity)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentException("Quantity must be positive.");

            var orderItem = new OrderItem(product.Id, product.Name, product.Price, quantity);

            OrderItems.Add(orderItem);

            // محاسبه مجدد مبلغ کل سفارش
            CalculateTotalAmount();
            SetUpdatedAt();
        }


    }
}
