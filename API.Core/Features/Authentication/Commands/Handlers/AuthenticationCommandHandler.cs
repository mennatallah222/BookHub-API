using API.Core.Bases;
using API.Core.Features.Authentication.Commands.Models;
using API.Core.SharedResource;
using API.Service.Interfaces;
using AutoMapper;
using ClassLibrary1.Data_ClassLibrary1.Core.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

public class AuthenticationCommandHandler : Response_Handler,
        IRequestHandler<SignInCommand, Response<JwtAuthResult>>,
        IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>,
        IRequestHandler<ResestPasswordCommand, Response<string>>

{
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _localizer;
    private readonly UserManager<ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity.User> _userManager;
    private readonly SignInManager<ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity.User> _signInManager;
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationCommandHandler(
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
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null) return BadRequest<JwtAuthResult>(_localizer[SharedResourceKeys.NotFound]);

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!user.EmailConfirmed)
            return BadRequest<JwtAuthResult>(_localizer[SharedResourceKeys.EmailNotConfirmed]);

        if (!signInResult.Succeeded)
        {
            return BadRequest<JwtAuthResult>(_localizer[SharedResourceKeys.PasswordNotCorrect]);
        }

        var accessToken = await _authenticationService.GetJWTToken(user);
        return Success(accessToken);
    }

    public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.AccessToken) || string.IsNullOrEmpty(request.RfreshToken))
        {
            return Unauthorized<JwtAuthResult>("Access or Refresh Token is missing.");
        }

        var token = _authenticationService.ReadJwtToken(request.AccessToken);
        var (userId, expireDate) = await _authenticationService.ValidateDetails(token, request.AccessToken, request.RfreshToken);

        switch (userId)
        {
            case "AlgorithmIsWrong":
                return Unauthorized<JwtAuthResult>(_localizer[SharedResourceKeys.AlgorithmIsWrong]);
            case "TokenIsNotExpired":
                return Unauthorized<JwtAuthResult>(_localizer[SharedResourceKeys.TokenIsNotExpired]);
            case "RefreshTokenIsNotFound":
                return Unauthorized<JwtAuthResult>(_localizer[SharedResourceKeys.RefreshTokenIsNotFound]);
            case "RefreshTokenIsExpired":
                return Unauthorized<JwtAuthResult>(_localizer[SharedResourceKeys.RefreshTokenIsExpired]);
        }

        if (expireDate == null)
        {
            return NotFound<JwtAuthResult>(_localizer[SharedResourceKeys.NotFound]);
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound<JwtAuthResult>(_localizer[SharedResourceKeys.NotFound]);
        }

        var result = await _authenticationService.GetRefreshToken(user, token, request.RfreshToken, expireDate);
        return Success(result);
    }

    public async Task<Response<string>> Handle(ResestPasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.SendResestPasswordCode(request.Email);
        switch (result)
        {
            case "UserNotFound":
                return Unauthorized<string>(_localizer[SharedResourceKeys.UserNotFound]);
            case "ErrorUpdatingTheUser":
                return Unauthorized<string>(_localizer[SharedResourceKeys.TryAgainAnotherTime]);

            default: return BadRequest<string>(result);
        }
    }
}
//} < PackageReference Include = "EntityFrameworkCore.EncryptColumn" Version = "6.0.8" />
