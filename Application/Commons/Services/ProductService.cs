using Application.Commons.Interfaces;
using Application.Commons.Models;
using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.Queries.GetProducts;
using Application.Features.Products.Queries.GetProductsWithPaginationAndFiltering;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Commons.Services
{
    public class ProductService(IRepositoryAsync<Product> repository, IMapper mapper) : IProductService
    {
        private readonly IRepositoryAsync<Product> _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<Response<Guid>> CreateProductAsync(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Product>(command);
            var product = await _repository.AddAsync(entity, cancellationToken);
            return new Response<Guid>(data: product.Id, message: ResponseMessages.EntityCreated);
        }

        public async Task<Response<ProductResponse>> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(id, cancellationToken);
            if (product is null)
                return new Response<ProductResponse>(message: ResponseMessages.EntityNotFound);

            var data = _mapper.Map<ProductResponse>(product);
            return new Response<ProductResponse>(data: data, message: ResponseMessages.RecordsRetrievedSuccessfully);
        }

        public async Task<PaginatedResponse<ProductResponse>> GetAllProductsWithPaginationAsync(FilterRequest filter, CancellationToken cancellationToken)
        {
            var specification = new GetProductsWithPaginationAndFilteringSpecificationQuery(filter.PageNumber, filter.PageSize, filter.Text);
            var products = await _repository.ListAsync(specification, cancellationToken);
            var totalCount = await _repository.CountAsync(specification, cancellationToken);

            var mappedProducts = _mapper.Map<IEnumerable<ProductResponse>>(products);
            return new PaginatedResponse<ProductResponse>(items: mappedProducts.ToList(), totalCount, filter.PageNumber, filter.PageSize);
        }

        public async Task<Response<IEnumerable<ProductResponse>>> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            var products = await _repository.ListAsync(new GetProductsSpecificationQuery(), cancellationToken);

            if (products.Count == 0)
                return new Response<IEnumerable<ProductResponse>>(message: ResponseMessages.EntityNotFound);

            var data = _mapper.Map<IEnumerable<ProductResponse>>(products);
            return new Response<IEnumerable<ProductResponse>>(data: data, message: ResponseMessages.RecordsRetrievedSuccessfully);
        }

        public async Task<Response<Guid>> UpdateProductAsync(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (product is null)
                return new Response<Guid>(message: ResponseMessages.EntityNotFound);

            _mapper.Map(command, product);
            await _repository.UpdateAsync(product, cancellationToken);

            return new Response<Guid>(data: command.Id, message: ResponseMessages.EntityUpdated);
        }

        public async Task<Response<Guid>> DeleteProductAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(id, cancellationToken);
            if (product is null)
                return new Response<Guid>(message: ResponseMessages.EntityNotFound);

            await _repository.DeleteAsync(product, cancellationToken);
            return new Response<Guid>(data: id, message: ResponseMessages.EntityDeleted);
        }
    }
}
