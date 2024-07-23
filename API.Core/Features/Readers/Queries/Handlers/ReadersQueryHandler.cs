using API.Core.Bases;
using API.Core.Features.Readers.Queries.Models;
using API.Core.Features.Readers.Queries.Responses;
using API.Core.SharedResource;
using API.Service.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.Readers.Queries.Handlers
{
    public class ReadersQueryHandler : Response_Handler,
        IRequestHandler<GetCurrentlyReadingList, GetCurrentlyReadingListResponse>,
        IRequestHandler<GetWantToReadListQuery, GetWantToReadListResponse>,
        IRequestHandler<GetReadListQuery, GetReadListResponse>

    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IReaderService _readerService;

        public ReadersQueryHandler(IMapper mapper,
                                     IReaderService readerService,
                                     IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
            _readerService = readerService;
        }

        public async Task<GetCurrentlyReadingListResponse> Handle(GetCurrentlyReadingList request, CancellationToken cancellationToken)
        {
            var user = await _readerService.GetUserWithCurrentlyReadingList(request.UserId);
            var currentlyReading = user.CurrentlyReading.Select(b => b.Name).ToList();

            return new GetCurrentlyReadingListResponse
            {
                CurrentlyReading = currentlyReading
            };
        }

        public async Task<GetWantToReadListResponse> Handle(GetWantToReadListQuery request, CancellationToken cancellationToken)
        {
            var user = await _readerService.GetUserWithWantToReadList(request.UserId);
            var wantToRead = user.WantToRead.Select(b => b.Name).ToList();

            return new GetWantToReadListResponse
            {
                WantToRead = wantToRead
            };
        }

        public async Task<GetReadListResponse> Handle(GetReadListQuery request, CancellationToken cancellationToken)
        {
            var user = await _readerService.GetUserWithReadList(request.UserId);
            var readList = user.ReadBooks.Select(b => b.Name).ToList();

            return new GetReadListResponse
            {
                ReadList = readList
            };
        }
    }
}
