using API.Core.Features.Queries.Models;
using API.Core.Features.Queries.Responses;
using AutoMapper;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Mapping.ProductMapping
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, GetAllProductsResponses>()
                .ForMember(dest=>dest.CategoryName, opt=>opt.MapFrom(src=>src.Category.Name));

        }
    }
}
