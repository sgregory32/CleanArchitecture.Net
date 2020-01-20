using AutoMapper;
using CleanArchitecture.Api.Models;
using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CategoryDTO, Category>();
            CreateMap<ProductDTO, Product>();
            CreateMap<CategoryProductDTO, Category>();
            CreateMap<ProductCategoryDTO, Product>();

            CreateMap<Category, CategoryDTO>();
            CreateMap<Product, ProductDTO>();
            CreateMap<Category, CategoryProductDTO>();
            CreateMap<Product, ProductCategoryDTO>();
        }

    }
}
