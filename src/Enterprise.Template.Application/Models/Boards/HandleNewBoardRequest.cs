using FluentValidation;

namespace Enterprise.Template.Application.Models.Boards
{
    public class HandleNewBoardRequest(int entityId, string? message)
    {
        public Guid CorrelationId { get; set; } = Guid.NewGuid();
        public int EntityId { get; set; } = entityId;
        public string? Message { get; set; } = message;
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
