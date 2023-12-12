using Application.Response;
using MediatR;

namespace Application.UseCases.GetBoards
{
    public class GetBoardsRequest : IRequest<ApiResponsePagination<GetBoardsResponse>>
    {
        public string? Name { get; set; }
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 25;
    }
}