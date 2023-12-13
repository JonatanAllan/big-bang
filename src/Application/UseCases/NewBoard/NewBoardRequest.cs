using FluentValidation;
using MediatR;

namespace Application.UseCases.NewBoard
{
    public class NewBoardRequest : IRequest<NewBoardResponse>
    {
        public required string Name { get; set; }
        public string? Description { get; set; }

        public Domain.Entities.Board ToEntity() => new(Name, Description);
    }

    public class NewBoardRequestValidator : AbstractValidator<NewBoardRequest>
    {
        public NewBoardRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3);
            RuleFor(x => x.Name)
                .MaximumLength(50);

            RuleFor(x => x.Description)
                .MaximumLength(500);
        }
    }
}
