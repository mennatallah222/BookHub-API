using API.Core.Features.Commands.Models;
using API.Core.Features.Commands.Responses;
using API.Core.Features.Queries.Responses;
using AutoMapper;
using ClassLibrary1.Data_ClassLibrary1.Core.DTOs;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;

namespace API.Core.Mapping.OrderMapping
{
    public class OrderProfile : Profile
    {

        public OrderProfile()
        {

            CreateMap<Cart, CreateOrderResponse>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.CartItems));

            CreateMap<CreateOrderCommand, Order>()
                .ForPath(dest => dest.User.Id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.OrderItems, opt => opt.Ignore())
                .ForMember(dest => dest.TotalAmount, opt => opt.Ignore())
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod));


            CreateMap<Order, CreateOrderResponse>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.OrderItems))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.User.Address))
                .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.PaymentStatus))
                ;


            CreateMap<OrderItem, CustomersOrdersDTO>()
                            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

            CreateMap<Order, OrderDTOs>()
                            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems.Select(o => new CustomersOrdersDTO
                            {
                                ProductId = o.Product.ProductId,
                                ProductName = o.Product.Name,
                            }).ToList()))
                            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount)); ;

            CreateMap<User, GetOrdersHistoryResponse>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders
                ));

        }
    }
}
