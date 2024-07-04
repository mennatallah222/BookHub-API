using API.Core.Features.Queries.Responses;
using AutoMapper;
using ClassLibrary1.Data_ClassLibrary1.Core.DTOs;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;

namespace API.Core.Mapping.CustomerMapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, GetCustomersResponse>()
                .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders))
                                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.Cart.CartItems));


            CreateMap<Product, ProductDto>();
            CreateMap<Product, CartItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name));
            CreateMap<CartItem, CartItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));

            CreateMap<Cart, GetCustomersResponse>()
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems));




            CreateMap<Order, OrderDTOs>();
            CreateMap<Product, CustomersOrdersDTO>();

            CreateMap<Customer, GetSingleCustomerResponse>()
                //.ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Orders.FirstOrDefault().Status))
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.Cart.CartItems))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
        }

    }
}
