namespace Application.UseCases.NewBoard
{
    public class NewBoardResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public NewBoardResponse(Domain.Entities.Board board)
        {
            Id = board.Id;
            Name = board.Name;
            Description = board.Description;
            CreatedAt = board.CreatedAt;
        }
    }
}
