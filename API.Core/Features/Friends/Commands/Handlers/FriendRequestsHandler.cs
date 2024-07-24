using API.Core.Bases;
using API.Core.Features.Friends.Commands.Models;
using API.Core.SharedResource;
using API.Service.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.Friends.Commands.Handlers
{
    public class FriendRequestsHandler : Response_Handler,
        IRequestHandler<SendFriendRequestQuery, Response<string>>,
        IRequestHandler<AcceptFriendRequestQuery, string>

    {

        private readonly IMapper _mapper;
        private readonly IFriendsService _friendsService;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public FriendRequestsHandler(IMapper mapper,
                                     IFriendsService friendsService,
                                     IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
            _friendsService = friendsService;
        }


        public async Task<Response<string>> Handle(SendFriendRequestQuery request, CancellationToken cancellationToken)
        {
            var result = await _friendsService.SendFriendRequestAsync(request.UserSenderId, request.FriendId);
            return Success(result);
        }

        public async Task<string> Handle(AcceptFriendRequestQuery request, CancellationToken cancellationToken)
        {
            var result = await _friendsService.AcceptFriendRequestAsync(request.UserAccepting, request.FriendId);
            return "Request accept successfully";
        }


    }
}
