using Asp.Versioning;
using Enterprise.Template.Application.Common.Response;
using Enterprise.Template.Application.Interfaces;
using Enterprise.Template.Application.Models.Boards;
using Microsoft.AspNetCore.Mvc;

namespace Enterprise.Template.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]
    public sealed class BoardController(IBoardApplication boardApplication, ILogger<BoardController> logger) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponsePagination<GetBoardsResponse>))]
        public async Task<IActionResult> Get([FromQuery] GetBoardsRequest request)
        {
            logger.LogInformation("Get boards");

            var response = await boardApplication.GetBoards(request);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(NewBoardResponse))]
        public async Task<IActionResult> Post([FromBody] NewBoardRequest request)
        {
            var response = await boardApplication.CreateBoard(request);
            return StatusCode(StatusCodes.Status201Created, response);
        }
    }
}
