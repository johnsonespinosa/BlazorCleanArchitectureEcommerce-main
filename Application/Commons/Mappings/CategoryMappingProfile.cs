using Application.Commons.Models;
using Application.Features.Categories.Commands.CreateCategory;
using Application.Features.Categories.Commands.UpdateCategory;
using Domain.Entities;

namespace Application.Commons.Mappings
{
    /// <summary>
    /// Mapping profile for categories.
    /// </summary>
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            // Mapping from CreateCategoryCommand to Category
            CreateMap<CreateCategoryCommand, Category>()
                .ForMember(destinationMember: category => category.Products, memberOptions: opt
                    => opt.Ignore()); // Ignore unnecessary properties
            
            // Mapping from UpdateCategoryCommand to Category
            CreateMap<UpdateCategoryCommand, Category>()
                .ForMember(destinationMember: category => category.Products, memberOptions: opt
                    => opt.Ignore()); // Ignore unnecessary properties
            
            // Mapping from Category to CategoryResponse
            CreateMap<Category, CategoryResponse>();
        }
    }
}