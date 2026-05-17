using SubscriptionHub.Domain.Common;
using SubscriptionHub.Domain.Enums;

namespace SubscriptionHub.Domain.Entities
{
    public class Tenant : AuditableEntity
    {
        public string Name { get; private set; }
        public string Slug { get; private set; }
        public string ContactEmail { get; private set; }
        public TenantStatus Status { get; private set; }

        private Tenant()
        {
            
        }

        public Tenant(string name,string slug, string contactEmail)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));
            if (string.IsNullOrWhiteSpace(slug))
                throw new ArgumentException("Slug is required.", nameof(slug));
            if (string.IsNullOrWhiteSpace(contactEmail))
                throw new ArgumentException("ContactEmail is required.", nameof(contactEmail));
            Name = name;
            Slug = slug;
            ContactEmail = contactEmail;
            Status = TenantStatus.Active;
        }

        public void Suspend()
        {
            if (Status == TenantStatus.Archived)
                throw new InvalidOperationException("Cannot suspend an archived tenant.");

            Status = TenantStatus.Suspended;
        }
        public void Reactivate()
        {
            if (Status == TenantStatus.Archived)
                throw new InvalidOperationException("Cannot reactivate an archived tenant.");

            Status = TenantStatus.Active;
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("name is required.", nameof(newName));
 
            Name = newName;
        }

        public void UpdateContactEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
                throw new ArgumentException("mail required.", nameof(newEmail));

            ContactEmail = newEmail;
        }

        public void Archive()
        {
            Status = TenantStatus.Archived;
        }
    }
}
