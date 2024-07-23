using API.Core.Bases;
using API.Core.Features.UserFeatures.Queries.Models;
using API.Core.Features.UserFeatures.Queries.Response;
using API.Core.SharedResource;
using API.Core.Wrappers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.UserFeatures.Queries.Handlers
{
    public class UserQueryHandler : Response_Handler,
                IRequestHandler<GetPaginatedUsersListQuery, PaginatedResult<GetUsersListResponse>>,
                IRequestHandler<GetUserByIDQuery, Response<GetUserByIDResponse>>

    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;

        private readonly UserManager<ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity.User> _userManager;

        public UserQueryHandler(IStringLocalizer<SharedResources> localizer,
                                  IMapper mapper,
                                  UserManager<ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity.User> userManager) : base(localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
            _userManager = userManager;

        }

        public async Task<PaginatedResult<GetUsersListResponse>> Handle(GetPaginatedUsersListQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users
                             .Include(u => u.CurrentlyReading)
                             .Include(u => u.ReadBooks)
                             .Include(u => u.WantToRead)
                             .AsQueryable();
            var paginatedList = await _mapper.ProjectTo<GetUsersListResponse>(users).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }

        public async Task<Response<GetUserByIDResponse>> Handle(GetUserByIDQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                             .Include(u => u.CurrentlyReading)
                             .Include(u => u.ReadBooks)
                             .Include(u => u.WantToRead)
                             .Include(x => x.Orders).ThenInclude(o => o.OrderItems)
                             .Include(c => c.Cart).ThenInclude(ci => ci.CartItems).ThenInclude(p => p.Product)
                             .FirstOrDefaultAsync(x => x.Id == request.Id);
            if (user == null) return NotFound<GetUserByIDResponse>(_localizer[SharedResourceKeys.NotFound]);
            var result = _mapper.Map<GetUserByIDResponse>(user);
            return Success(result);
        }
    }
}
//    return await _customers.Include(x => x.Orders).ThenInclude(o => o.OrderItems)
//                           .Include(c => c.Cart).ThenInclude(ci => ci.CartItems).ThenInclude(p => p.Product)
//                           .FirstOrDefaultAsync(i => i.CustomerId == id);