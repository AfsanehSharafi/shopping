using Domain.Common;

namespace Domain.Entities
{
    public class Product:AggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; } // موجودی انبار
        public bool IsActive { get; private set; }

        // Foreign Key & Navigation Property for Category
        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; }

        private Product() { }

        public Product(string name, string description, decimal price, int stockQuantity, Guid categoryId)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty.", nameof(name));
            if(price < 0)
                throw new ArgumentException("Price cannot be negative.", nameof(price));
            if(stockQuantity < 0)
                throw new ArgumentException("Initial stock cannot be negative.", nameof(stockQuantity));
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
            CategoryId = categoryId;
            IsActive = true;
        }


        public void UpdateDetails(string name, string description, decimal price)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");
            if(price < 0)
                throw new ArgumentException("Price cannot be negative.");

            Name = name;
            Description = description;
            Price = price;
            SetUpdatedAt();

        }


        //  کاهش موجودی موقع خرید
        public void ReduceStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity to reduce must be positive.");
            if (StockQuantity < quantity)
                throw new InvalidOperationException("Not enough stock available.");

            StockQuantity -= quantity;
            SetUpdatedAt();
        }


        // افزایش موجودی مثلاً: موقع شارژ انبار
        public void AddStock(int quantity)
        {
            if(quantity <= 0)
                throw new ArgumentException("Quantity to add must be positive.");
            
            StockQuantity += quantity;
            SetUpdatedAt();
        }


        public void ChangePrice(decimal newPrice)
        {
            if(Price < 0)
                throw new ArgumentException("New price cannot be negative.");

            Price = newPrice;
            SetUpdatedAt();
        }

        public void Activate()
        {
            IsActive = true;
            SetUpdatedAt();
        }

        public void Deactivate()
        {
            IsActive = false;
            SetUpdatedAt();
        }

    }
}
