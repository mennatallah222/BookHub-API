using API.Core.Bases;
using API.Core.Features.Authorization.Queries.Models;
using API.Core.Features.Authorization.Queries.Responses;
using API.Core.SharedResource;
using API.Service.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.Authorization.Queries.Handlers
{
    public class RoleHadnler : Response_Handler,
        IRequestHandler<GetRoleListQuery, Response<List<GetRoleListResponse>>>,
        IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResponse>>

    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IAuthorizationService _authorizationService;

        public RoleHadnler(IMapper mapper,
                           IAuthorizationService authorizationService,
                           IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _mapper = mapper;
            _localizer = stringLocalizer;
            _authorizationService = authorizationService;
        }
        public async Task<Response<List<GetRoleListResponse>>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authorizationService.GetRolesList();
            var result = _mapper.Map<List<GetRoleListResponse>>(roles);
            return Success(result);
        }

        public async Task<Response<GetRoleByIdResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.GetRolesById(request.Id);
            if (role == null) return NotFound<GetRoleByIdResponse>(_localizer[SharedResourceKeys.NotFound]);
            var result = _mapper.Map<GetRoleByIdResponse>(role);
            return Success(result);
        }
    }
}
