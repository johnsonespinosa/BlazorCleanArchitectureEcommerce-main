using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.DeleteProduct;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.Queries.GetProductById;
using Application.Features.Products.Queries.GetProductsWithPagination;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ISender _sender;

        public ProductsController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Response<Guid>>> Create([FromBody] CreateProductCommand command)
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
        public async Task<ActionResult<PaginatedResponse<ProductResponse>>> GetWithPagination([FromQuery] PaginationRequest request)
        {
            var response = await _sender.Send(new GetProductsWithPaginationQuery(request));
            return Ok(response);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Response<ProductResponse>>> GetById(Guid id)
        {
            var response = await _sender.Send(new GetProductByIdQuery(id));
            if (!response.Succeeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Response<Guid>>> Update([FromBody] UpdateProductCommand command)
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

        [Authorize]
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<Response<Guid>>> Delete(Guid id)
        {
            var response = await _sender.Send(new DeleteProductCommand(id));
            if (!response.Succeeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
