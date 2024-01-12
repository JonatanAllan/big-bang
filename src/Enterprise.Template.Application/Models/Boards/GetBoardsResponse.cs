using Enterprise.Template.Domain.Entities;

namespace Enterprise.Template.Application.Models.Boards
{
    public class GetBoardsResponse(Board board)
    {
        public int Id { get; set; } = board.Id;
        public string Name { get; set; } = board.Name;
        public DateTime CreatedAt { get; set; } = board.CreatedAt;
        public DateTime? LastUpdatedAt { get; set; } = board.LastUpdatedAt;
    }
}