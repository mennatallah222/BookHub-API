
//using API.Core.Bases;
//using API.Core.Features.User.Commands.Models;
//using API.Core.Shared;
//using AutoMapper;
//using MediatR;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Localization;

//namespace API.Core.Features.User.Commands.Handlers
//{
//    public class UserCommandHandler : Response_Handler,
//        IRequestHandler<AddUserCommand, Response<string>>
//    {
//        private readonly IMapper _mapper;
//        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
//        private readonly UserManager<>
//        public UserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
//                                  IMapper mapper) : base(stringLocalizer)
//        {
//            _mapper = mapper;
//            _stringLocalizer = stringLocalizer;
//        }
//        public Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
//        {

//        }
//    }
//}
