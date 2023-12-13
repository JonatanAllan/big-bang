using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Response;
using Core.Extensions;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.UseCases.GetBoards
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
            var predicate = PredicateBuilder.True<Board>();
            if (!string.IsNullOrEmpty(request.Name))
                predicate = predicate.And(s => s.Name.Contains(request.Name, StringComparison.InvariantCultureIgnoreCase));

            var boards = await _boardRepository.GetManyAsync(predicate, request.Skip, request.Take);
            var total = await _boardRepository.CountAsync(predicate);
            var items = boards.Select(s => new GetBoardsResponse(s)).ToList();
            return new ApiResponsePagination<GetBoardsResponse>(items, total);
        }
    }
}
