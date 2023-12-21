using MediatR;

namespace CaliberFS.Template.Application.UseCases.HandleNewBoard
{
    public class HandleNewBoardUseCase : IRequestHandler<HandleNewBoardRequest, Unit>
    {
        public Task<Unit> Handle(HandleNewBoardRequest request, CancellationToken cancellationToken)
        {
            // Todo: Implement logic
            return Unit.Task;
        }
    }
}
