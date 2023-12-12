using Application.Services;
using Application.UseCases.NewBoard;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.UseCases.V1.Board.NewBoard
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public sealed class BoardController : ControllerBase, IOutputPort
    {
        private readonly IMediator _mediator;
        private readonly Notification<IOutputPort> _notification;

        private IActionResult? _viewModel;

        void IOutputPort.Invalid()
        {
            var problemDetails = new ValidationProblemDetails(_notification.Errors);
            _viewModel = BadRequest(problemDetails);
        }

        void IOutputPort.Ok(Domain.Entities.Board board) => _viewModel = Ok(new NewBoardResponse(board));

        public BoardController(IMediator mediator, Notification<IOutputPort> notification)
        {
            _mediator = mediator;
            _notification = notification;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewBoardRequest request)
        {
            _notification.SetOutputPort(this);
            await _mediator.Send(request);
            return _viewModel!;
        }
    }
}
