using SubscriptionHub.Domain.Common;
using SubscriptionHub.Domain.Enums;

namespace SubscriptionHub.Domain.Entities
{
    public class User : AuditableEntity
    {

        public Guid TenantId { get; private set; }
        
        public string Email { get; private set; } = string.Empty;
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;

        public UserRole Role { get; private set; }

        public bool IsActive { get; private set; }

        private User() { }
        public User(Guid tenantId, string email, string firstName, string lastName, UserRole role)
        {
            if (tenantId == Guid.Empty)
                throw new ArgumentException("TenantId is required.", nameof(tenantId));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.", nameof(email));
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name is required.", nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("last name is required.", nameof(lastName));
            if(role == UserRole.None)
                throw new ArgumentException("Role is required.", nameof(role));

            TenantId = tenantId;
            Email = email;
            FirstName = firstName;
            LastName = lastName;

            Role = role;
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void UpdateName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name is required.", nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("last name is required.", nameof(lastName));

            FirstName = firstName;
            LastName = lastName;
        }

        public void ChangeRole(UserRole role)
        {
            Role = role;
        }

    }
}
