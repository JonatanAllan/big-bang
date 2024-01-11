using Enterprise.Template.Application.Models.Base;
using FluentValidation;
using FluentValidation.Results;

namespace Enterprise.Template.Application.Models.Boards
{
    public class NewBoardRequest : IValidationRequest
    {
        public required string Name { get; set; }
        public string? Description { get; set; }

        public Domain.Entities.Board ToEntity() => new(Name, Description);
        public Task<ValidationResult> Validate()
        {
            return new NewBoardRequestValidator().ValidateAsync(this);
        }
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
