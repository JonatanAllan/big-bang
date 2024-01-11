using Enterprise.Template.Application.Common.Response;
using Enterprise.Template.Application.Models.Boards;
using Enterprise.Template.Application.UseCases.GetBoards;
using Enterprise.Template.Application.UseCases.NewBoard;

namespace Enterprise.Template.Application.Interfaces
{
    public interface IBoardApplication
    {
        Task<ApiResponsePagination<GetBoardsResponse>> GetBoards(GetBoardsRequest request);
        Task<NewBoardResponse> CreateBoard(NewBoardRequest request);
        Task HandleNewBoard(HandleNewBoardRequest request);
    }
}
