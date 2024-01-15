using Enterprise.Operations;
using Enterprise.Template.Application.Common.Response;
using Enterprise.Template.Application.Models.Boards;

namespace Enterprise.Template.Application.Interfaces
{
    public interface IBoardApplication
    {
        Task<ApiResponsePagination<GetBoardsResponse>> GetBoards(GetBoardsRequest request);
        Task<NewBoardResponse> CreateBoard(NewBoardRequest request);
        Task<OperationResult> HandleNewBoard(HandleNewBoardRequest request);
    }
}
