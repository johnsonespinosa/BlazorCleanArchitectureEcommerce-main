using Application.Commons.Models;
using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.UpdateProduct;
using Domain.Entities;

namespace Application.Commons.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            // Mapping CreateProductCommand to Product
            CreateMap<CreateProductCommand, Product>()
                .ForMember(destinationMember: product => product.Id, memberOptions: opt
                    => opt.Ignore()) // Ignore Id when creating
                .ForMember(destinationMember: product => product.Category, memberOptions: opt
                    => opt.Ignore()); // Ignore the relationship with Category (can be handled manually)

            // Mapping UpdateProductCommand to Product
            CreateMap<UpdateProductCommand, Product>();

            // Mapping Product to ProductResponse
            CreateMap<Product, ProductResponse>()
                .ForMember(destinationMember: productResponse => productResponse.Category, memberOptions: opt
                    => opt.MapFrom(mapExpression: product => new CategoryResponse 
                {
                    Id = product.Category!.Id,
                    Name = product.Category.Name
                }));
        }
    }
}