using CaliberFS.Template.Application.Common.Response;
using CaliberFS.Template.Domain.Interfaces.Repositories;
using MediatR;

namespace CaliberFS.Template.Application.UseCases.GetBoards
{
    public class GetBoardsUseCase : IRequestHandler<GetBoardsRequest, ApiResponsePagination<GetBoardsResponse>>
    {
        private readonly IBoardRepository _boardRepository;

        public GetBoardsUseCase(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public async Task<ApiResponsePagination<GetBoardsResponse>> Handle(GetBoardsRequest request, CancellationToken cancellationToken)
        {
            var boards = (await _boardRepository.GetByNameAsync(request.Name)).Skip(request.Skip).Take(request.Take);
            var total = await _boardRepository.CountByNameAsync(request.Name);

            var items = boards.Select(s => new GetBoardsResponse(s)).ToList();

            return new ApiResponsePagination<GetBoardsResponse>(items, total);
        }
    }
}
