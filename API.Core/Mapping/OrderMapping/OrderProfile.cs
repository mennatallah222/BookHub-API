using API.Core.Features.Commands.Models;
using API.Core.Features.Commands.Responses;
using API.Core.Features.Queries.Responses;
using AutoMapper;
using ClassLibrary1.Data_ClassLibrary1.Core.DTOs;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;

namespace API.Core.Mapping.OrderMapping
{
    public class OrderProfile : Profile
    {
        IMapper _mapper;

        public OrderProfile()
        {

            CreateMap<Cart, CreateOrderResponse>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.CartItems));

            CreateMap<CreateOrderCommand, Order>()
                .ForPath(dest => dest.Customer.CustomerId, opt => opt.MapFrom(src => src.CustomerID))
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore())
                .ForMember(dest => dest.TotalAmount, opt => opt.Ignore())
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod));


            CreateMap<Order, CreateOrderResponse>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.OrderItems))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.Customer.Address))
                .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.PaymentStatus))
                ;

            CreateMap<Order, OrderDTOs>();

            CreateMap<Product, CustomersOrdersDTO>();


            CreateMap<Customer, GetOrdersHistoryResponse>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders
                ));

            CreateMap<Order, GetOrdersHistoryResponse>();
            CreateMap<List<Order>, GetOrdersHistoryResponse>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.FirstOrDefault().CustomerId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstOrDefault().Customer.Name))
                .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src));


            CreateMap<Order, CreateOrderResponse>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.OrderItems))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.Customer.Address))
                .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.PaymentStatus));



        }
    }
}
