using CaliberFS.Template.Application.Common.Exceptions;
using CaliberFS.Template.Application.Services.RabbitMQ;
using CaliberFS.Template.Application.Services.UnitOfWork;
using CaliberFS.Template.Core.RabbitMQ.Producer;
using CaliberFS.Template.Domain.Interfaces.Repositories;
using MediatR;

namespace CaliberFS.Template.Application.UseCases.NewBoard
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
            await _boardRepository.AddAsync(board);

            _unitOfWork.Commit();
            producer.Publish(new SampleIntegrationEvent(board.Id, "Board created with success."));
            return new NewBoardResponse(board);
        }

        private async Task Validate(NewBoardRequest request)
        {
            var count = await _boardRepository.CountByNameAsync(request.Name);

            if (count > 0)
                throw new ValidationException("Board","Already exists");
        }
       
    }
}
