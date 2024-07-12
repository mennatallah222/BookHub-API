using API.Core.Bases;
using API.Core.Features.Authentication.Commands.Models;
using API.Core.SharedResource;
using API.Service.Interfaces;
using AutoMapper;
using ClassLibrary1.Data_ClassLibrary1.Core.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler : Response_Handler,
            IRequestHandler<SignInCommand, Response<JwtAuthResult>>,
            IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>
    {

        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly UserManager<ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity.User> _userManager;
        private readonly SignInManager<ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity.User> _signInManager;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationCommandHandler(IProductsService productsService,
                                     IMapper mapper,
                                     UserManager<ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity.User> userManager,
                                     SignInManager<ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity.User> signInManager,
                                     IAuthenticationService authenticationService,
                                  IStringLocalizer<SharedResources> localizer) : base(localizer)

        {
            _mapper = mapper;
            _localizer = localizer;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationService = authenticationService;

        }
        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //check if user exists, then return username not found if not
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return BadRequest<JwtAuthResult>(_localizer[SharedResourceKeys.NotFound]);

            //try to sign in, if failed, retun password is wrong
            var signInResult = _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!signInResult.IsCompletedSuccessfully)
            {
                return BadRequest<JwtAuthResult>(_localizer[SharedResourceKeys.PasswordNotCorrect]);

            }
            //generate token then return it
            var accessToken = await _authenticationService.GetJWTToken(user);

            return Success(accessToken);
        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.GetRefreshToken(request.AccessToken, request.RfreshToken);
            return Success(result);
        }
    }
}
