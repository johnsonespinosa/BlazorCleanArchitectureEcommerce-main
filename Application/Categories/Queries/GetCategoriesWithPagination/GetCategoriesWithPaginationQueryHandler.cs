﻿using Application.Commons.Exceptions;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Models;
using MediatR;

namespace Application.UseCases.Categories.Queries.GetCategoriesWithPagination
{
    public sealed class GetCategoriesWithPaginationQueryHandler : IRequestHandler<GetCategoriesWithPaginationQuery, PaginatedResponse<CategoryResponse>>
    {
        private readonly IRepositoryAsync<Category> _repository;
        private readonly IMapper _mapper;
        public GetCategoriesWithPaginationQueryHandler(IRepositoryAsync<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedResponse<CategoryResponse>> Handle(GetCategoriesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAsync(request.Filter, cancellationToken);
            if (entities is null || entities.TotalCount < 0)
                throw new NotFoundException();

            var response = _mapper.Map<PaginatedResponse<CategoryResponse>>(entities);
            return response;
        }
    }
}
