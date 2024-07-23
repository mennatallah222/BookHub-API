using API.Core.Features.Readers.Queries.Responses;
using AutoMapper;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;

namespace API.Core.Mapping.ReaderMapping
{
    public class ReaderProfile : Profile
    {
        public ReaderProfile()
        {
            CreateMap<User, GetCurrentlyReadingListResponse>()
            .ForMember(dest => dest.CurrentlyReading, opt => opt.MapFrom(src => src.CurrentlyReading.Select(b => b.Name).ToList()));

        }
    }
}
