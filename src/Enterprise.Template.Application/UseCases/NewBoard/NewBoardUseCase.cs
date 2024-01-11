using Enterprise.Template.Application.Common.Exceptions;
using Enterprise.Template.Application.Services.RabbitMQ;
using Enterprise.Template.Application.Services.UnitOfWork;
using Enterprise.Template.Core.RabbitMQ.Producer;
using Enterprise.Template.Domain.Interfaces.Repositories;
using MediatR;

namespace Enterprise.Template.Application.UseCases.NewBoard
{
    public class NewBoardUseCase(
        IBoardRepository boardRepository, 
        IUnitOfWork unitOfWork, 
        IRabbitMqProducer<SampleIntegrationEvent> producer) : IRequestHandler<NewBoardRequest, NewBoardResponse>
    {
        public async Task<NewBoardResponse> Handle(NewBoardRequest request, CancellationToken cancellationToken)
        {
            await Validate(request);
            var board = request.ToEntity();
            await boardRepository.AddAsync(board);
            unitOfWork.SaveChanges();
            producer.Publish(new SampleIntegrationEvent(board.Id, "Board created with success."));
            return new NewBoardResponse(board);
        }

        private async Task Validate(NewBoardRequest request)
        {
            var boardExists = await boardRepository.ExistsAsync(x => x.Name.Equals(request.Name, StringComparison.InvariantCulture));
            if (boardExists)
                throw new ValidationException("Board","Already exists");
        }
       
    }
}
