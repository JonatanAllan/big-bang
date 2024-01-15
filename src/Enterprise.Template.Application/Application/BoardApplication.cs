using Enterprise.Operations;
using Enterprise.PubSub.Enums;
using Enterprise.Template.Domain.Interfaces.Repositories;
using Enterprise.Template.Application.Common.Exceptions;
using Enterprise.Template.Application.Services.UnitOfWork;
using Enterprise.Template.Application.Common.Response;
using Enterprise.Template.Core.Extensions;
using Enterprise.Template.Domain.Entities;
using Enterprise.Template.Application.Models.Boards;
using Enterprise.Template.Application.Interfaces;
using Enterprise.PubSub.Interfaces;
using Enterprise.Template.Domain.Constants;
using Microsoft.Extensions.Logging;

namespace Enterprise.Template.Application.Application
{
    public class BoardApplication(
        ILogger<BoardApplication> logger,
        IBoardRepository boardRepository, 
        IUnitOfWork unitOfWork,
        IPublisherService publisherService) : IBoardApplication
    {
        public async Task<ApiResponsePagination<GetBoardsResponse>> GetBoards(GetBoardsRequest request)
        {
            var validation = await request.Validate();
            if (!validation.IsValid)
                throw new ValidationException(validation.Errors);

            var predicate = PredicateBuilder.True<Board>();
            if (!string.IsNullOrEmpty(request.Name))
                predicate = predicate.And(s => s.Name.Contains(request.Name, StringComparison.InvariantCultureIgnoreCase));

            var boards = await boardRepository.GetManyAsync(predicate, request.Skip, request.Take);
            var total = await boardRepository.CountAsync(predicate);
            var items = boards.Select(s => new GetBoardsResponse(s)).ToList();
            return new ApiResponsePagination<GetBoardsResponse>(items, total);
        }

        public async Task<NewBoardResponse> CreateBoard(NewBoardRequest request)
        {
            await Validate(request);
            var board = request.ToEntity();
            await boardRepository.AddAsync(board);
            unitOfWork.SaveChanges();
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
            if(!validation.IsValid)
                throw new ValidationException(validation.Errors);

            var boardExists = await boardRepository.ExistsAsync(x => x.Name.Equals(request.Name, StringComparison.InvariantCulture));
            if (boardExists)
                throw new ValidationException("Board", "Already exists");
        }
    }
}
