using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.DeleteProduct;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.Queries.GetProductById;
using Application.Features.Products.Queries.GetProductsWithPagination;
using Application.Models;
using MediatR;
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

        [HttpPost]
        public async Task<WritingResponse> Create([FromBody] CreateProductCommand command)
            => await _sender.Send(command);

        [HttpGet]
        public async Task<PaginatedResponse<ProductResponse>> GetWithPagination([FromQuery] PaginationRequest request)
            => await _sender.Send(new GetProductsWithPaginationQuery(request));

        [HttpGet("guid:id")]
        public async Task<ProductResponse> GetById(Guid id)
            => await _sender.Send(new GetProductByIdQuery(id));

        [HttpPut]
        public async Task<WritingResponse> Update([FromBody] UpdateProductCommand command)
            => await _sender.Send(command);

        [HttpDelete("guid:id")]
        public async Task<WritingResponse> Delete(Guid id)
            => await _sender.Send(new DeleteProductCommand(id));
    }
}
