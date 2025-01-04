using Application.Commons.Models;
using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.DeleteProduct;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.Queries.GetProductById;
using Application.Features.Products.Queries.GetProductsWithPaginationAndFiltering;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route(template: "api/[controller]")]
    [ApiController]
    public class ProductsController(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender ?? throw new ArgumentNullException(nameof(sender));
        
        // [Authorize(Roles = "Administrator")]
        [HttpPost(template: "Create")]
        public async Task<ActionResult<Response<Guid>>> Create([FromBody] CreateProductCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(error: new Response<Guid>(errors: ModelState.Values
                    .SelectMany(entry => entry.Errors.Select(error => error.ErrorMessage)).ToArray()));
            }

            var response = await _sender.Send(command);
            
            if (!response.Succeeded)
                return BadRequest(response);

            return CreatedAtAction(nameof(GetById), new { id = response.Data }, response);
        }

        [HttpGet(template: "GetWithPagination")]
        public async Task<ActionResult<PaginatedResponse<ProductResponse>>> GetWithPagination([FromQuery] FilterRequest filterRequest)
        {
            var request = new GetProductsWithPaginationAndFilteringQuery()
            {
                Filter = filterRequest.Text,
                PageSize = filterRequest.PageSize,
                PageNumber = filterRequest.PageNumber,
            };
            var response = await _sender.Send(request);
            return Ok(response);
        }

        [HttpGet(template: "GetById/{id}")]
        public async Task<ActionResult<Response<ProductResponse>>> GetById(Guid id)
        {
            var request = new GetProductByIdQuery(id);
            var response = await _sender.Send(request);
            
            if (!response.Succeeded)
                return NotFound(response);

            return Ok(response);
        }

        // [Authorize(Roles = "Administrator")]
        [HttpPut(template: "Update")]
        public async Task<ActionResult<Response<Guid>>> Update([FromBody] UpdateProductCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(error: new Response<Guid>(errors: ModelState.Values
                    .SelectMany(entry => entry.Errors.Select(error => error.ErrorMessage)).ToArray()));
            }

            var response = await _sender.Send(command);
            
            if (!response.Succeeded)
                return NotFound(response);

            return Ok(response);
        }

        // [Authorize(Roles = "Administrator")]
        [HttpDelete(template: "Delete/{id}")]
        public async Task<ActionResult<Response<Guid>>> Delete(Guid id)
        {
            var request = new DeleteProductCommand(id);
            
            var response = await _sender.Send(request);
            
            if (!response.Succeeded)
                return NotFound(response);

            return Ok(response);
        }
    }
}
