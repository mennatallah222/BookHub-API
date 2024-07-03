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
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Orders.First().Status))
                .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders));

            CreateMap<Order, OrderDTOs>();
            CreateMap<Product, CustomersOrdersDTO>();

            /*CreateMap<Order, GetCustomersResponse>()
                .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.OrderId));*/
            CreateMap<Customer, GetSingleCustomerResponse>()
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Orders.FirstOrDefault().Status))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Orders.SelectMany(o => o.Products.Select(p => p.Name)).ToList()));
        }
    }
}
