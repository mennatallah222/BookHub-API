using API.Core.Bases;
using API.Core.Features.Authorization.Commands.Models;
using API.Core.SharedResource;
using API.Service.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.Authorization.Commands.Handlers
{
    public class AddRoleCommandHandler : Response_Handler,
                                         IRequestHandler<AddRoleCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IAuthorizationService _authorizationService;

        public AddRoleCommandHandler(IMapper mapper,
                                     IAuthorizationService authorizationService,
                                     IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
            _authorizationService = authorizationService;
        }

        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.AddRoleAsync(request.RoleName);
            if (result == "Successeeded") return Success(result);
            return BadRequest<string>(_localizer[SharedResourceKeys.AddedFailed]);

        }
    }
}
