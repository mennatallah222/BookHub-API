using API.Core.Bases;
using API.Core.Features.Authorization.Commands.Models;
using API.Core.SharedResource;
using API.Service.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.Authorization.Commands.Handlers
{
    public class ClaimsCommandHandler : Response_Handler,
        IRequestHandler<UpdateUserClaimsCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IAuthorizationService _authorizationService;

        public ClaimsCommandHandler(IMapper mapper,
                                     IAuthorizationService authorizationService,
                                     IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
            _authorizationService = authorizationService;
        }
        public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserClaims(request);
            switch (result)
            {
                case ("UserIsNull"): return NotFound<string>(_localizer[SharedResourceKeys.UserNotFound]);
                case ("FailedToRemoveClaims"): return NotFound<string>(_localizer[SharedResourceKeys.FailedToRemoveClaims]);
                case ("FailedToAddNewClaims"): return NotFound<string>(_localizer[SharedResourceKeys.FailedToAddNewClaims]);
                case ("FailedToUpdateClaims"): return NotFound<string>(_localizer[SharedResourceKeys.FailedToUpdateClaims]);
            }
            return Success<string>(_localizer[SharedResourceKeys.Success]);
        }
    }
}
