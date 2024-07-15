
using API.Core.Bases;
using API.Core.Features.Authorization.Queries.Responses;
using MediatR;

namespace API.Core.Features.Authorization.Queries.Models
{
    public class GetRoleListQuery : IRequest<Response<List<GetRoleListResponse>>>
    {
    }
}
