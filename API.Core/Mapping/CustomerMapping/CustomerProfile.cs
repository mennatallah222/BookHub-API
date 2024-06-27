using API.Core.Features.Queries.Responses;
using AutoMapper;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Mapping.CustomerMapping
{
    public class CustomerProfile :Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, GetCustomersResponse>()
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Orders.First().Status))
                .ForMember(dest=> dest.Products, opt=> opt.MapFrom(src=>src.Orders.SelectMany(o=>o.Products.Select(p=>p.Name)).ToList()));
        }
    }
}
