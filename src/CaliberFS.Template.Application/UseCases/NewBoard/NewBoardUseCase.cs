using CaliberFS.Template.Application.Common.Exceptions;
using CaliberFS.Template.Application.Services.UnitOfWork;
using CaliberFS.Template.Domain.Interfaces.Repositories;
using MediatR;

namespace CaliberFS.Template.Application.UseCases.NewBoard
{
    public class NewBoardUseCase(IBoardRepository boardRepository, IUnitOfWork unitOfWork) : IRequestHandler<NewBoardRequest, NewBoardResponse>
    {
        private readonly IBoardRepository _boardRepository = boardRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<NewBoardResponse> Handle(NewBoardRequest request, CancellationToken cancellationToken)
        {
            await Validate(request);
            var board = request.ToEntity();
            await _boardRepository.AddAsync(board);
            _unitOfWork.SaveChanges();

            return new NewBoardResponse(board);
        }

        private async Task Validate(NewBoardRequest request)
        {
            var boardExists = await _boardRepository.ExistsAsync(x => x.Name.Equals(request.Name, StringComparison.InvariantCulture));
            if (boardExists)
                throw new ValidationException("Board","Already exists");
        }
       
    }
}
