using Application.Features.BlogPosts.Commands.Create;
using Application.Features.BlogPosts.Commands.Delete;
using Application.Features.BlogPosts.Commands.Update;
using Application.Features.BlogPosts.Queries.GetById;
using Application.Features.BlogPosts.Queries.GetList;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogPostsController : BaseController
{
    [HttpGet("get/{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdBlogPostQuery getByIdBlogPostQuery)
    {
        GetByIdBlogPostResponse result = await Mediator.Send(getByIdBlogPostQuery);
        return Ok(result);
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListBlogPostQuery getListBlogPostQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListBlogPostListItemDto> result = await Mediator.Send(getListBlogPostQuery);
        return Ok(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Add([FromBody] CreateBlogPostCommand createBlogPostCommand)
    {
        CreatedBlogPostResponse result = await Mediator.Send(createBlogPostCommand);
        return Created(uri: "", result);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteBlogPostCommand deleteBlogPostCommand)
    {
        DeletedBlogPostResponse result = await Mediator.Send(deleteBlogPostCommand);
        return Ok(result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateBlogPostCommand updateBlogPostCommand)
    {
        UpdatedBlogPostResponse result = await Mediator.Send(updateBlogPostCommand);
        return Ok(result);
    }
}
