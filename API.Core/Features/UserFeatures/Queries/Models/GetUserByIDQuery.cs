using API.Core.Bases;
using API.Core.Features.UserFeatures.Queries.Response;
using MediatR;

namespace API.Core.Features.UserFeatures.Queries.Models
{
    public class GetUserByIDQuery : IRequest<Response<GetUserByIDResponse>>
    {
        public int Id { get; set; }
        public GetUserByIDQuery(int id)
        {
            Id = id;
        }
    }
}
