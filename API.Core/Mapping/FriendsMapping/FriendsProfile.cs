using AutoMapper;
using ClassLibrary1.Data_ClassLibrary1.Core.DTOs;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;

namespace API.Core.Mapping.FriendsMapping
{
    public class FriendsProfile : Profile
    {
        public FriendsProfile()
        {
            CreateMap<Friendship, FriendshipDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.FriendName, opt => opt.MapFrom(src => src.Friend.UserName));


            CreateMap<User, UserDto>();


        }
    }
}
