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
        public async Task<WritingResponse> Create([FromBody] CreateCategoryCommand command)
            => await _sender.Send(command);

        [HttpGet]
        public async Task<PaginatedResponse<CategoryResponse>> GetWithPagination([FromQuery] PaginationRequest request)
            => await _sender.Send(new GetCategoriesWithPaginationQuery(request));

        [HttpGet("guid:id")]
        public async Task<CategoryResponse> GetById(Guid id)
            => await _sender.Send(new GetCategoryByIdQuery(id));

        [HttpPut]
        public async Task<WritingResponse> Update([FromBody] UpdateCategoryCommand command)
            => await _sender.Send(command);

        [HttpDelete("guid:id")]
        public async Task<WritingResponse> Delete(Guid id)
            => await _sender.Send(new DeleteCategoryCommand(id));
    }
}
