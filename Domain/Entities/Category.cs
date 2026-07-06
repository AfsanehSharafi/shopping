using Domain.Common;

namespace Domain.Entities
{
    public class Category:AggregateRoot
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

        public Category(string name, string? description = null, Guid? parentCategoryId = null)
        {
            if(string.IsNullOrWhiteSpace(name))
            
                throw new ArgumentException("Category Name cannot be empty.", nameof(name));
            
            Name = name;
            Description = description;
            ParentCategoryId = parentCategoryId;
        }

        public void UpdateDetails(string name , string? description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category Name cannot be empty.", nameof(name));

            Name += name;
            Description += description;
            SetUpdatedAt();

            
        }

        public void ChangeParent(Guid? newParentCategoryId)
        {
            ParentCategoryId = newParentCategoryId;
            SetUpdatedAt();
        }

    }
}
