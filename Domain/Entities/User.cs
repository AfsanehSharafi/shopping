using Domain.Common;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Domain.Entities
{
    public class User : AggregateRoot
    {
        // Properties for Users
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; private set; }
        public string? PhoneNumber { get; private set; }
        public DateTime DateOfBirth { get; private set;}
        public Role UserRole { get; private set; }

        // Navigation Properties (if any, for relationships)
        // Example: A user can have multiple orders
        public ICollection<Order> Orders { get; private set; } = new List<Order>();

        // Private constructor for EF Core
        private User() { }


        // Public constructor to create a new user
        public User(string firstName, string lastName , string email , string passwordHash, DateTime dateOfBirth, Role userRole = Role.Customer)
        {
            // Basic Validations (can be moved to FluentValidation later)
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First Name cannot be empty." , nameof(firstName));
            if(string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("First Name cannot be empty." , nameof(lastName));
            if(string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty.", nameof(email));
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Password hash cannot be empty.", nameof(passwordHash));
            if(dateOfBirth >=  DateTime.UtcNow)
                throw new ArgumentException("Date of birth must be in the past.", nameof(dateOfBirth));

            Id= Guid.NewGuid();
            FirstName= firstName;
            LastName= lastName;
            Email= email;
            PasswordHash= passwordHash;
            DateOfBirth = dateOfBirth;
            UserRole= userRole;
            // CreatedAt is set by BaseEntity
        }

        // Methods to modify the User state (Encapsulation)
        public void UpdateName(string firstName , string lastName)
        {
            if(string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("first name can not be empty." , nameof(firstName));
            if(string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("last name can not be empty.", nameof(lastName));

            FirstName= firstName;
            LastName= lastName;
            SetUpdatedAt(); // Mark as Updated
        }


    }
}
