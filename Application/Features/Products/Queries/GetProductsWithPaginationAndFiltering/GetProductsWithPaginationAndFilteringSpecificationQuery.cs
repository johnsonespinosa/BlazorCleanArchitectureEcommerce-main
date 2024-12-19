﻿using Ardalis.Specification;
using Domain.Entities;

namespace Application.Features.Products.Queries.GetProductsWithPaginationAndFiltering
{
    public class GetProductsWithPaginationAndFilteringSpecificationQuery : Specification<Product>
    {
        public GetProductsWithPaginationAndFilteringSpecificationQuery(int pageNumber, int pageSize, string? filter)
        {
            if (!string.IsNullOrWhiteSpace(filter))
                Query.Where(product => product.Name!.Contains(filter) || product.Description!.Contains(filter));

            Query.Skip((pageNumber - 1) * pageSize).Take(pageSize).Include(product => product.Category);
        }
    }
}