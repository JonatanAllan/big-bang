using Enterprise.Template.Application.Common.Exceptions;
using Enterprise.Template.Application.Common.Response;
using Enterprise.Template.Application.Interfaces;
using Enterprise.Template.Application.Models.Boards;
using Enterprise.Template.Application.Services.RabbitMQ;
using Enterprise.Template.Application.Services.UnitOfWork;
using Enterprise.Template.Core.RabbitMQ.Producer;
using Enterprise.Template.Domain.Interfaces.Repositories;

namespace Enterprise.Template.Application.Application
{
    public class BoardApplication(IBoardRepository boardRepository, IRabbitMqProducer<SampleIntegrationEvent> producer) : IBoardApplication
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

            producer.Publish(new SampleIntegrationEvent(board.Id, "Board created with success."));
            return new NewBoardResponse(board);
        }

        public Task HandleNewBoard(HandleNewBoardRequest request)
        {
           return Task.CompletedTask;
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
