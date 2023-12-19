using CaliberFS.Template.Application.Common.Response;
using FluentValidation;
using MediatR;

namespace CaliberFS.Template.Application.UseCases.GetBoards
{
    public class GetBoardsRequest : IRequest<ApiResponsePagination<GetBoardsResponse>>
    {
        public string? Name { get; set; } = string.Empty;

        public int Skip { get; set; } = 0;

        public int Take { get; set; } = 25;

        public GetBoardsRequest() { }

        public GetBoardsRequest(string name, int skip, int take)
        {
            Name = name;
            Skip = skip;
            Take = take;
        }
    }

    public class GetBoardRequestValidator : AbstractValidator<GetBoardsRequest>
    {
        public GetBoardRequestValidator()
        {
            RuleFor(x => x.Skip).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Take) .GreaterThanOrEqualTo(1);
            RuleFor(x => x.Take).LessThanOrEqualTo(50);
            RuleFor(x => x.Name)
                .MinimumLength(3)
                .When(x => x != null);
        }
    }
}