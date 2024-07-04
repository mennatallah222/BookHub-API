using AutoMapper;

namespace API.Core.Mapping.OrderMapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {


            /* CreateMap<CreateOrderCommand, Order>()
                 .ForPath(dest => dest.Customer.CustomerId, opt => opt.MapFrom(src => src.CustomerID))
                 .ForMember(dest => dest.Products, opt => opt.Ignore())
                 .ForMember(dest => dest.TotalAmount, opt => opt.Ignore())
                 .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod));


             CreateMap<Order, CreateOrderResponse>()
                 .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                 .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
                 .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod))
                 .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                 .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                 .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.Customer.Address))
                 .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.PaymentStatus))
                 .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));
             ;
 */
        }
    }
}
