﻿using Application.Interfaces;
using Application.Models;
using Domain.Models;
using MediatR;

namespace Application.UseCases.Products.Commands.DeleteProduct
{
    public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, WritingResponse>
    {
        private readonly IRepositoryAsync<Product> _repository;

        public DeleteProductCommandHandler(IRepositoryAsync<Product> repository)
        {
            _repository = repository;
        }

        public async Task<WritingResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var response = await _repository.DeleteAsync(request.Id, cancellationToken);
            return response;
        }
    }
}
