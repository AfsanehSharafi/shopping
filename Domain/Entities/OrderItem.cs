using Domain.Common;

namespace Domain.Entities
{
    public class OrderItem: Entity
    {
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; } 
        public decimal UnitPrice { get; private set; }  // کپی کردن قیمت در لحظه خرید
        public int Quantity { get; private set; }
        public decimal TotalPrice => UnitPrice * Quantity; // محاسبه خودکار قیمت کل این ردیف

        // Navigation Property
        public Product Product { get; private set; }

        // Private constructor for EF Core
        private OrderItem() { }

        public OrderItem(Guid productId, string productName, decimal unitPrice, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");
            if (unitPrice < 0)
                throw new ArgumentException("Unit price cannot be negative.");

            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

    }
}
