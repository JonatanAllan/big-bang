using Enterprise.Operations;
using Enterprise.PubSub.Enums;
using Enterprise.PubSub.Interfaces;
using Enterprise.Template.Application.Common.Exceptions;
using Enterprise.Template.Application.Common.Response;
using Enterprise.Template.Application.Interfaces;
using Enterprise.Template.Application.Models.Boards;
using Enterprise.Template.Domain.Constants;
using Enterprise.Template.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace Enterprise.Template.Application.Application
{
    public class BoardApplication(
        ILogger<BoardApplication> logger,
        IBoardRepository boardRepository,
        IPublisherService publisherService) : IBoardApplication
    {
        public async Task<ApiResponsePagination<GetBoardsResponse>> GetBoards(GetBoardsRequest request)
        {
            var validation = await request.Validate();
            if (!validation.IsValid)
                throw new ValidationException(validation.Errors);

            var boards = await boardRepository.GetManyAsync(request.Name, request.Skip, request.Take);
            var total = await boardRepository.CountAsync(request.Name);

            var items = boards.Select(s => new GetBoardsResponse(s)).ToList();
            return new ApiResponsePagination<GetBoardsResponse>(items, total);
        }

        public async Task<NewBoardResponse> CreateBoard(NewBoardRequest request)
        {
            await Validate(request);
            var board = request.ToEntity();
            
            await boardRepository.AddAsync(board);

            var message = new HandleNewBoardRequest(board.Id,$"{board.Name} created");
            var result = await publisherService.PublishMessageAsync(message, PublishType.Queue, Queues.SampleMessage);

            if (result.Failed)
                logger.LogError(result.Message);

            return new NewBoardResponse(board);
        }

        public async Task<OperationResult> HandleNewBoard(HandleNewBoardRequest request)
        {
           return await Task.FromResult(OperationResult.Ok());
        }

        private async Task Validate(NewBoardRequest request)
        {
            var validation = await request.Validate();

            if (!validation.IsValid)
                throw new ValidationException(validation.Errors);

            var boardExists = await boardRepository.ExistsAsync(request.Name);

            if (boardExists)
                throw new ValidationException("Board", "Already exists");
        }
    }
}
