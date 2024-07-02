using API.Core.Features.Commands.Models;
using API.Core.Features.Queries.Models;
using API.Core.Features.Queries.Responses;
using AutoMapper;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;

namespace API.Core.Mapping.ProductMapping
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, GetAllProductsResponses>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            /*CreateMap<CreateProductCommand, Product>()
                .ForMember(dest => dest.Category.Name, opt => opt.MapFrom(src => src.CategoryName));
            */
            CreateMap<CreateProductCommand, Product>()
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    if (!string.IsNullOrEmpty(src.CategoryName))
                    {
                        dest.Category = new Category { Name = src.CategoryName };
                    }
                });

        }
    }
}
