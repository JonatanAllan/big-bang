using CaliberFS.Template.Application.UseCases.NewBoard;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CaliberFS.Template.WebApi.UseCases.V1.Board.NewBoard
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]
    public sealed class BoardController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BoardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(NewBoardResponse))]
        public async Task<IActionResult> Post([FromBody] NewBoardRequest request)
        {
            var response = await _mediator.Send(request);
            return StatusCode(StatusCodes.Status201Created, response);
        }
    }
 }