using API.Core.Features.UserFeatures.Commands.Models;
using API.Core.Features.UserFeatures.Queries.Response;
using AutoMapper;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;

namespace API.Core.Mapping.UserMapping
{
    public partial class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AddUserCommand, User>();
            CreateMap<UpdateUserCommand, User>();


            CreateMap<User, GetUsersListResponse>()
                .ForMember(dest => dest.CurrentlyReading, opt => opt.MapFrom(src => src.CurrentlyReading.Select(b => b.Name).ToList()))
                .ForMember(dest => dest.ReadList, opt => opt.MapFrom(src => src.ReadBooks.Select(b => b.Name).ToList()))
                .ForMember(dest => dest.WantToReadList, opt => opt.MapFrom(src => src.WantToRead.Select(b => b.Name).ToList()));



            CreateMap<User, GetUserByIDResponse>()
                .ForMember(dest => dest.CurrentlyReading, opt => opt.MapFrom(src => src.CurrentlyReading.Select(b => b.Name).ToList()))
                .ForMember(dest => dest.ReadList, opt => opt.MapFrom(src => src.ReadBooks.Select(b => b.Name).ToList()))
                .ForMember(dest => dest.WantToReadList, opt => opt.MapFrom(src => src.WantToRead.Select(b => b.Name).ToList()));
        }
    }
}