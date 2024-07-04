using API.Core.Features.Queries.Responses;
using AutoMapper;
using ClassLibrary1.Data_ClassLibrary1.Core.DTOs;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;

namespace API.Core.Mapping.CartMapping
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Product, CartItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name));
            CreateMap<CartItem, CartItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));

            CreateMap<Cart, GetCartResponse>()
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems));



        }

    }
}

/*
  CreateMap<Product, CartItem>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Name));

            CreateMap<Cart, GetCartResponse>()
               .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems));
 */