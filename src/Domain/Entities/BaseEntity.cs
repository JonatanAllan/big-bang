namespace Domain.Entities
{
    public interface IAggregateRoot
    {
        void Validate();
    }
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? LastUpdatedAt { get; protected set; }

        public void CreateAtNow() => CreatedAt = DateTime.Now;
        public void UpdateAtNow() => LastUpdatedAt = DateTime.Now;

    }
}
