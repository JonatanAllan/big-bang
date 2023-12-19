namespace CaliberFS.Template.Domain.Entities
{
    public class Board : BaseEntity, IAggregateRoot
    {
        public Board(string name, string? description)
        {
            Name = name;
            Description = description;
        }
        public string Name { get; private set; }
        public string? Description { get; private set; }

        public virtual ICollection<TaskItem>? Tasks { get; private set; }

        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
