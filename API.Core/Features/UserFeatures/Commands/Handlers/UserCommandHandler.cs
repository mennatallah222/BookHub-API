using API.Core.Bases;
using API.Core.Features.UserFeatures.Commands.Models;
using API.Core.SharedResource;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.UserFeatures.Commands.Handlers
{
    public class UserCommandHandler : Response_Handler,
        IRequestHandler<AddUserCommand, Response<string>>
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
    }
}
