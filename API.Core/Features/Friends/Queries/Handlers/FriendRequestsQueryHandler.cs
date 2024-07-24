using API.Core.Bases;
using API.Core.Features.Friends.Queries.Models;
using API.Core.SharedResource;
using API.Service.Interfaces;
using AutoMapper;
using ClassLibrary1.Data_ClassLibrary1.Core.DTOs;
using MediatR;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.Friends.Queries.Handlers
{
    public class FriendRequestsQueryHandler : Response_Handler,
        IRequestHandler<GetFriendRequestQuery, List<FriendshipDto>>,
        IRequestHandler<GetFriendQuery, List<FriendshipDto>>
    {

        private readonly IMapper _mapper;
        private readonly IFriendsService _friendsService;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public FriendRequestsQueryHandler(IMapper mapper,
                                     IFriendsService friendsService,
                                     IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
            _friendsService = friendsService;
        }

        public async Task<List<FriendshipDto>> Handle(GetFriendRequestQuery request, CancellationToken cancellationToken)
        {
            var friendships = await _friendsService.GetPendingFriendRequestsAsync(request.UserId);

            return _mapper.Map<List<FriendshipDto>>(friendships);
        }
        public async Task<List<FriendshipDto>> Handle(GetFriendQuery request, CancellationToken cancellationToken)
        {
            var friendships = await _friendsService.GetAcceptedFriendRequestsAsync(request.UserId);
            return _mapper.Map<List<FriendshipDto>>(friendships);
        }


    }
}
