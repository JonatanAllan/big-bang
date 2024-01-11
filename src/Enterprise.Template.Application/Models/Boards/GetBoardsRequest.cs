using Enterprise.Template.Application.Models.Base;
using FluentValidation;
using FluentValidation.Results;

namespace Enterprise.Template.Application.Models.Boards
{
    public class GetBoardsRequest : IValidationRequest
    {
        public string? Name { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; } = 25;
        public GetBoardsRequest() { }

        public GetBoardsRequest(string? name, int skip, int take)
        {
            Name = name;
            Skip = skip;
            Take = take;
        }

        public Task<ValidationResult> Validate()
        {
            return new GetBoardRequestValidator().ValidateAsync(this);
        }
    }

    public class GetBoardRequestValidator : AbstractValidator<GetBoardsRequest>
    {
        public GetBoardRequestValidator()
        {
            RuleFor(x => x.Skip).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Take).GreaterThanOrEqualTo(1);
            RuleFor(x => x.Take).LessThanOrEqualTo(50);
            RuleFor(x => x.Name)
                .MinimumLength(3);
        }
    }
}