using API.Core.Bases;
using API.Core.Features.UserFeatures.Commands.Models;
using API.Core.SharedResource;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.UserFeatures.Commands.Handlers
{

    public class UserCommandHandler : Response_Handler,
        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<UpdateUserCommand, Response<string>>,
        IRequestHandler<DeleteUserCommand, Response<string>>


    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity.User> _userManager;

        public UserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                  IMapper mapper,
                                  UserManager<ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity.User> userManager) : base(stringLocalizer)
        {
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
        }

        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            // If email already exists
            var userEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userEmail != null)
            {
                return BadRequest<string>(_stringLocalizer[SharedResourceKeys.EmailExists]);
            }

            // If username already exists
            var username = await _userManager.FindByNameAsync(request.UserName);
            if (username != null)
            {
                return BadRequest<string>(_stringLocalizer[SharedResourceKeys.UserNameExists]);
            }

            // Map request to User entity
            var identityUser = _mapper.Map<ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity.User>(request);

            // Create user
            var createdUser = await _userManager.CreateAsync(identityUser, request.Password);
            if (!createdUser.Succeeded)
            {
                return BadRequest<string>(createdUser.Errors.FirstOrDefault()?.Description);
            }

            return Created("");
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
            return Success<string>(_stringLocalizer[SharedResourceKeys.Updated]);

        }
    }
}
