namespace Enterprise.Template.Application.Models.Boards
{
    public class NewBoardResponse(Domain.Entities.Board board)
    {
        public int Id { get; set; } = board.Id;
        public string Name { get; set; } = board.Name;
        public string? Description { get; set; } = board.Description;
        public DateTime CreatedAt { get; set; } = board.CreatedAt;
    }
}
