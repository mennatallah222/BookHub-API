using API.Core.Bases;
using MediatR;

namespace API.Core.Features.Readers.Queries.Models
{
    public class GetCurrentlyReadingList : IRequest<Response<string>>
    {
    }
}
