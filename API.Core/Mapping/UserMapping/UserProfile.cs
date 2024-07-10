using API.Core.Features.UserFeatures.Commands.Models;
using AutoMapper;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;

namespace API.Core.Mapping.UserMapping
{
    public partial class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AddUserCommand, User>()
                ;
        }
    }
}