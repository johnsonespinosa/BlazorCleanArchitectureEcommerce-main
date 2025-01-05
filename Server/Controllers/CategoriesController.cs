using Application.Commons.Models;
using Application.Features.Categories.Commands.CreateCategory;
using Application.Features.Categories.Commands.DeleteCategory;
using Application.Features.Categories.Commands.UpdateCategory;
using Application.Features.Categories.Queries.GetCategories;
using Application.Features.Categories.Queries.GetCategoriesWithPaginationAndFiltering;
using Application.Features.Categories.Queries.GetCategoryById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route(template: "api/[controller]")]
    [ApiController]
    public class CategoriesController(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender ?? throw new ArgumentNullException(nameof(sender));

        // [Authorize(Roles = "Administrator")]
        [HttpPost(template: "Create")]
        public async Task<ActionResult<Response<Guid>>> Create([FromBody] CreateCategoryCommand command)
        {
            var response = await _sender.Send(command);
            
            if (!response.Succeeded)
                return BadRequest(response.Errors);
            
            return CreatedAtAction(nameof(GetById), routeValues: new { id = response.Data }, value: response);
        }
        
        [HttpGet(template: "GetAll")]
        public async Task<ActionResult<PaginatedResponse<CategoryResponse>>> GetAll()
        {
            var request = new GetCategoriesQuery();
            var response = await _sender.Send(request);
            return Ok(response);
        }

        [HttpGet(template: "GetWithPagination")]
        public async Task<ActionResult<PaginatedResponse<CategoryResponse>>> GetWithPagination([FromQuery] FilterRequest filterRequest)
        {
            var request = new GetCategoriesWithPaginationAndFilteringQuery(filterRequest);
            var response = await _sender.Send(request);
            return Ok(response);
        }

        [HttpGet(template: "GetById/{id}")]
        public async Task<ActionResult<Response<CategoryResponse>>> GetById(Guid id)
        {
            var request = new GetCategoryByIdQuery(id);
            var response = await _sender.Send(request);

            if (!response.Succeeded)
                return NotFound(response);

            return Ok(response);
        }

        // [Authorize(Roles = "Administrator")]
        [HttpPut(template: "Update")]
        public async Task<ActionResult<Response<Guid>>> Update([FromBody] UpdateCategoryCommand command)
        {
            var response = await _sender.Send(command);
            
            if (!response.Succeeded)
                return NotFound(response);

            return Ok(response);
        }

        // [Authorize(Roles = "Administrator")]
        [HttpDelete(template: "Delete/{id}")]
        public async Task<ActionResult<Response<Guid>>> Delete(Guid id)
        {
            var request = new DeleteCategoryCommand(id);
            
            var response = await _sender.Send(request);
            
            if (!response.Succeeded)
                return NotFound(response);

            return Ok(response);
        }
    }
}
