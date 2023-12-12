﻿using Application.Response;
using Application.UseCases.GetBoards;
using Application.UseCases.NewBoard;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.UseCases.V1.Board.GetBoards
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
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
