﻿using API.Core.Bases;
using API.Core.Features.Authentication.Queries.Models;
using API.Core.SharedResource;
using API.Service.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.Authentication.Queries.Handlers
{
    public class AuthenticationQueryHandler : Response_Handler,
            IRequestHandler<AuthorizeUserQuery, Response<string>>
    {

        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationQueryHandler(IProductsService productsService,
                                     IMapper mapper,
                                     IAuthenticationService authenticationService,
                                  IStringLocalizer<SharedResources> localizer) : base(localizer)

        {
            _mapper = mapper;
            _localizer = localizer;
            _authenticationService = authenticationService;

        }

        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ValidateToken(request.AccessToken);
            if (result == "NotExpired")
            {
                return Success(result);
            }
            return BadRequest<string>("Expired");
        }
    }
}
