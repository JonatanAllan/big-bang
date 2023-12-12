using Application.Services;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.UnitOfWork;
using MediatR;

namespace Application.UseCases.NewBoard
{
    public class NewBoardUseCase : IRequestHandler<NewBoardRequest, Unit>
    {
        private readonly Notification<IOutputPort> _notification;
        private readonly IBoardRepository _boardRepository;
        private readonly IUnitOfWork _unitOfWork;

        public NewBoardUseCase(Notification<IOutputPort> notification, IBoardRepository boardRepository, IUnitOfWork unitOfWork)
        {
            _boardRepository = boardRepository;
            _unitOfWork = unitOfWork;
            _notification = notification;
        }

        public async Task<Unit> Handle(NewBoardRequest request, CancellationToken cancellationToken)
        {
            var board = request.ToEntity();
            await _boardRepository.AddAsync(board);
            _unitOfWork.SaveChanges();
            _notification.Output?.Ok(board);
            return Unit.Value;
        }
       
    }
}
