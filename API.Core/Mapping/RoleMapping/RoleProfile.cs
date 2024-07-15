using API.Core.Features.Authorization.Queries.Responses;
using AutoMapper;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;

namespace API.Core.Mapping.RoleMapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {

            CreateMap<Role, GetRoleListResponse>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name));

            CreateMap<Role, GetRoleByIdResponse>()
                            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name));

            CreateMap<GetRoleListResponse, GetRoleByIdResponse>();

        }
    }
}
