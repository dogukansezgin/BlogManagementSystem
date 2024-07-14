using Application.Features.Comments.Commands.Create;
using Application.Features.Comments.Commands.Delete;
using Application.Features.Comments.Commands.Update;
using Application.Features.Comments.Queries.GetById;
using Application.Features.Comments.Queries.GetList;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : BaseController
{
    [HttpGet("get/{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdCommentQuery getByIdCommentQuery)
    {
        GetByIdCommentResponse result = await Mediator.Send(getByIdCommentQuery);
        return Ok(result);
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCommentQuery getListCommentQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListCommentListItemDto> result = await Mediator.Send(getListCommentQuery);
        return Ok(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Add([FromBody] CreateCommentCommand createCommentCommand)
    {
        CreatedCommentResponse result = await Mediator.Send(createCommentCommand);
        return Created(uri: "", result);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteCommentCommand deleteCommentCommand)
    {
        DeletedCommentResponse result = await Mediator.Send(deleteCommentCommand);
        return Ok(result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateCommentCommand updateCommentCommand)
    {
        UpdatedCommentResponse result = await Mediator.Send(updateCommentCommand);
        return Ok(result);
    }
}
