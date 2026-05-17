namespace SubscriptionHub.Domain.Common
{
    public abstract class  AuditableEntity : Entity
    {
        public DateTime CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        protected AuditableEntity() : base() { }
        protected AuditableEntity(Guid id) : base(id) { }
    }
}
