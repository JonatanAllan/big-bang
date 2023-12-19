using CaliberFS.Template.Application.Common.Response;
using CaliberFS.Template.Application.UseCases.GetBoards;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CaliberFS.Template.WebApi.UseCases.V1.Board.GetBoards
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponsePagination<GetBoardsResponse>))]
        public async Task<IActionResult> Get([FromQuery] GetBoardsRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
