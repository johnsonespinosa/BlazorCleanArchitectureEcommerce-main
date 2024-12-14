using Application.Features.Categories.Commands.CreateCategory;
using Application.Features.Categories.Commands.DeleteCategory;
using Application.Features.Categories.Commands.UpdateCategory;
using Application.Features.Categories.Queries.GetCategoriesWithPagination;
using Application.Features.Categories.Queries.GetCategoryById;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ISender _sender;

        public CategoriesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<ActionResult<Response<Guid>>> Create([FromBody] CreateCategoryCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response<Guid>(ModelState.Values
                    .SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToArray()));
            }

            var response = await _sender.Send(command);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return CreatedAtAction(nameof(GetById), new { id = response.Data }, response);
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<CategoryResponse>>> GetWithPagination([FromQuery] PaginationRequest request)
        {
            var response = await _sender.Send(new GetCategoriesWithPaginationQuery(request));
            return Ok(response);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Response<CategoryResponse>>> GetById(Guid id)
        {
            var response = await _sender.Send(new GetCategoryByIdQuery(id));
            if (!response.Succeeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<Response<Guid>>> Update([FromBody] UpdateCategoryCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response<Guid>(ModelState.Values
                    .SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToArray()));
            }

            var response = await _sender.Send(command);
            if (!response.Succeeded)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<Response<Guid>>> Delete(Guid id)
        {
            var response = await _sender.Send(new DeleteCategoryCommand(id));
            if (!response.Succeeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
