namespace Domain.Entities
{
    public class Board : BaseEntity, IAggregateRoot
    {
        public Board() { }

        public Board(string name, string? description)
        {
            Id = Random.Shared.Next(1, 100);
            Name = name;
            Description = description;
        }

        public string Name { get; private set; } = string.Empty;

        public string? Description { get; private set; }

        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
