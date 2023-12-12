using MediatR;

namespace Application.UseCases.NewBoard
{
    public class NewBoardRequest : IRequest<NewBoardResponse>
    {
        public required string Name { get; set; }
        public string? Description { get; set; }

        public Domain.Entities.Board ToEntity() => new(Name, Description);
    }
}
