using Enterprise.Template.Domain.Enums;

namespace Enterprise.Template.Domain.Entities
{
    public class TaskItem : BaseEntity, IAggregateRoot
    {
        public TaskItem(string title, string? description, TaskItemStatus status, int boardId)
        {
            Title = title;
            Description = description;
            Status = status;
            BoardId = boardId;
        }
        protected TaskItem() { }

        public string Title { get; private set; }
        public string? Description { get; private set; }
        public TaskItemStatus Status { get; private set; }
        public DateTime? DueDate { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public int BoardId { get; private set; }
        public virtual Board? Board { get; private set; }

        public void Validate()
        {
            throw new NotImplementedException();
        }

        public void Complete()
        {
            Status = TaskItemStatus.Done;
            CompletedAt = DateTime.Now;
        }

        public bool IsOverdue() => !IsDone() && DueDate.HasValue && DueDate.Value < DateTime.Now;
        public bool IsToDo() => Status == TaskItemStatus.ToDo;
        public bool IsInProgress() => Status == TaskItemStatus.InProgress;
        public bool IsDone() => Status == TaskItemStatus.Done;
    }
}
