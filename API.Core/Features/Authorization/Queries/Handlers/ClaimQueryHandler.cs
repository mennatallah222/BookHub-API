using API.Core.Bases;
using API.Core.Features.Authorization.Queries.Models;
using API.Core.SharedResource;
using API.Service.Interfaces;
using AutoMapper;
using ClassLibrary1.Data_ClassLibrary1.Core.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.Authorization.Queries.Handlers
{
    public class ClaimQueryHandler : Response_Handler,
        IRequestHandler<ManageUserClaimQuery, Response<ManageUserClaimResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity.User> _userManager;

        public ClaimQueryHandler(IMapper mapper,
                           UserManager<ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity.User> userManager,
                           IAuthorizationService authorizationService,

                           IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _mapper = mapper;
            _localizer = stringLocalizer;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }
        public async Task<Response<ManageUserClaimResponse>> Handle(ManageUserClaimQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) return NotFound<ManageUserClaimResponse>(_localizer[SharedResourceKeys.UserNotFound]);
            var result = await _authorizationService.ManageUserClaimssData(user);
            return Success(result);
        }
    }
}
