using FluentValidation;
using MediatR;

namespace Enterprise.Template.Application.UseCases.HandleNewBoard
{
    public class HandleNewBoardRequest : IRequest<Unit>
    {
        public Guid CorrelationId { get; set; }
        public int EntityId { get; set; }
        public string? Message { get; set; }
    }

    public class HandleNewBoardRequestValidator : AbstractValidator<HandleNewBoardRequest>
    {
        public HandleNewBoardRequestValidator()
        {
            RuleFor(x => x.CorrelationId).NotEmpty();
            RuleFor(x => x.Message).NotEmpty();
        }
    }
}
