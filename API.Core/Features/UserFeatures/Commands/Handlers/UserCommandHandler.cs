using API.Core.Bases;
using API.Core.Features.UserFeatures.Commands.Models;
using API.Core.SharedResource;
using API.Service.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.UserFeatures.Commands.Handlers
{

    public class UserCommandHandler : Response_Handler,
        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<UpdateUserCommand, Response<string>>,
        IRequestHandler<DeleteUserCommand, Response<string>>,
        IRequestHandler<ChangeUserPasswordCommand, Response<string>>



    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity.User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IEmailService _emailService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IApplicationUserService _applicationUserService;

        public UserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                  IMapper mapper,
                                  UserManager<ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity.User> userManager,
                                  IHttpContextAccessor contextAccessor,
                                  IEmailService emailService,
                                  IAuthenticationService authenticationService,
                                  IApplicationUserService applicationUserService) : base(stringLocalizer)
        {
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _emailService = emailService;
            _authenticationService = authenticationService;
            _applicationUserService = applicationUserService;
        }

        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var identityUser = _mapper.Map<ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity.User>(request);

            var createdUser = await _applicationUserService.AddUserAsync(identityUser, request.Password, request.Image);

            switch (createdUser)
            {

                case "EmailExists": return BadRequest<string>(_stringLocalizer[SharedResourceKeys.EmailExists]);
                case "UserNameExists": return BadRequest<string>(_stringLocalizer[SharedResourceKeys.UserNameExists]);
                case "ErrorInCreateUser": return BadRequest<string>(_stringLocalizer[SharedResourceKeys.FailedToAddUser]);
                case "Success": return Success<string>(_stringLocalizer[SharedResourceKeys.Success]);
                case "Failed": return BadRequest<string>(_stringLocalizer[SharedResourceKeys.TryToRegisterAgain]);
                default: return BadRequest<string>(createdUser);
            }
        }

        public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (user == null)
            {
                return BadRequest<string>(_stringLocalizer[SharedResourceKeys.NotFound]);
            }
            user.FullName = request.FullName;
            user.Email = request.Email;
            user.UserName = request.UserName;
            user.Address = request.Address;
            user.PhoneNumber = request.PhoneNumber;
            user.Country = request.Country;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest<string>(result.Errors.FirstOrDefault()?.Description);

            }
            return Success<string>(_stringLocalizer[SharedResourceKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (user == null)
            {
                return BadRequest<string>(_stringLocalizer[SharedResourceKeys.NotFound]);
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest<string>(result.Errors.FirstOrDefault()?.Description);

            }
            return Success<string>(_stringLocalizer[SharedResourceKeys.Deleted]);

        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (user == null)
            {
                return NotFound<string>(_stringLocalizer[SharedResourceKeys.NotFound]);
            }

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPasswrod, request.NewPasswrod);
            if (!result.Succeeded)
            {
                return BadRequest<string>(_stringLocalizer[SharedResourceKeys.ChangePasswordFailed]);

            }
            return Success<string>(_stringLocalizer[SharedResourceKeys.Updated]);
        }
    }
}
