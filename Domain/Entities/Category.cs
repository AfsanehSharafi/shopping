namespace Domain.Entities
{
    public class Category
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }

        // Parent Category برای ساخت ساختار درختی (Hierarchical Structure)
        // یعنی یک دسته‌بندی می‌تواند زیرمجموعه یک دسته‌بندی دیگر باشد
        public Guid? ParentCategoryId { get; private set; }
        public Category? ParentCategory { get; private set; }

        // Navigation Property: لیست زیرمجموعه‌های این دسته
        public ICollection<Category> SubCategories { get; private set; } = new List<Category>();

        // Navigation Property: لیست محصولاتی که در این دسته هستند
        public ICollection<Product> Products { get; private set; } = new List<Product>();

        // Private constructor for EF Core
        private Category() { }

    }
}
