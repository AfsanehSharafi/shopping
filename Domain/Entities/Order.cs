using Domain.Common;
using Domain.Events;

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

        public ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();

        private Order() { }

        public Order(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            OrderDate = DateTime.UtcNow;
            Status = OrderStatus.Pending;
            TotalAmount = 0;

            // رویداد ایجاد سفارش
            AddDomainEvent(new OrderCreatedEvent(Id));
        }

        public void AddOrderItem(Product product, int quantity)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentException("Quantity must be positive.");

            var orderItem = new OrderItem(product.Id, product.Name, product.Price, quantity);

            OrderItems.Add(orderItem);

            CalculateTotalAmount();
            SetUpdatedAt();
        }

        public void CancelledOrder()
        {
            if (Status == OrderStatus.Shipped || Status == OrderStatus.Delivered)
                throw new InvalidOperationException("Cannot cancel an order that has already been shipped or delivered.");

            Status = OrderStatus.Cancelled;
            SetUpdatedAt();

            // رویداد لغو سفارش
            AddDomainEvent(new OrderCancelledEvent(Id));
        }

        public void MarkAsPaid()
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Only pending orders can be marked as paid.");

            Status = OrderStatus.Paid;
            SetUpdatedAt();

            // رویداد پرداخت موفق
            AddDomainEvent(new OrderPaidEvent(Id));
        }

        private void CalculateTotalAmount()
        {
            TotalAmount = OrderItems.Sum(item => item.TotalPrice);
        }
    }
}
