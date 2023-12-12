using Application.Services;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.UseCases.NewBoard
{
    public class NewBoardValidationUseCase : IRequestHandler<NewBoardRequest, Unit>
    {
        private readonly IBoardRepository _boardRepository;
        private readonly Notification<IOutputPort> _notification;

        private readonly IRequestHandler<NewBoardRequest, Unit> _inner;

        public NewBoardValidationUseCase(IRequestHandler<NewBoardRequest, Unit> inner, IBoardRepository boardRepository, Notification<IOutputPort> notification)
        {
            _boardRepository = boardRepository;
            _notification = notification;
            _inner = inner;
        }

        public async Task<Unit> Handle(NewBoardRequest request, CancellationToken cancellationToken)
        {
            var boardExists = await _boardRepository.ExistsAsync(x => string.Equals(x.Name, request.Name, StringComparison.CurrentCultureIgnoreCase));
            if (boardExists)
                _notification.Add(nameof(Board), "Duplicated name.");

            if (!_notification.HasErrors) return await _inner.Handle(request, cancellationToken);

            _notification.Output?.Invalid();
            return Unit.Value;

        }
    }
}
