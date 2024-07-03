using API.Core.Features.Commands.Models;
using API.Core.Features.Commands.Responses;
using AutoMapper;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;

namespace API.Core.Mapping.OrderMapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {


            CreateMap<CreateOrderCommand, Order>()
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

        }
    }
}
/*CreateMap<CreateOrderCommand, Order>()
                .ForPath(dest => dest.Customer.CustomerId, opt => opt.MapFrom(src => src.CustomerID))
                .ForMember(dest => dest.Products, opt => opt.Ignore())
                .AfterMap(async (src, dest, context) =>
                {
                    var productRepo = context.Items["ProductRepo"] as IProductRepo;
                    var pids = src.Products.Select(p => p.ProductId).ToList();
                    var products = productRepo.GetProductsByIDS(pids);
                    products.Wait();

                    dest.TotalAmount = 0;
                    foreach (var op in src.Products)
                    {
                        var p = dest.Products.FirstOrDefault(pid => pid.ProductId == op.ProductId);
                        if (p != null)
                        {
                            p.Quantity -= op.Quantity;
                            dest.TotalAmount += p.Price * op.Quantity;
                            //dest.Products.Add(p);
                        }
                        else
                        {
                            throw new Exception($"Product with ID {op.ProductId} not found in the order.");
                        }
                    }
                })
                ;*/