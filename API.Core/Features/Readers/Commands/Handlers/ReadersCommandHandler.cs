using API.Core.Bases;
using API.Core.Features.Readers.Commands.Models;
using API.Core.SharedResource;
using API.Service.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.Readers.Commands.Handlers
{
    public class ReadersCommandHandler : Response_Handler,
        IRequestHandler<AddBookToCurrentlyReadingListCommand, Response<string>>,
        IRequestHandler<AddBookToReadListCommand, Response<string>>,
        IRequestHandler<AddBookToWantToReadListCommand, Response<string>>,
        IRequestHandler<RemoveBookFromCurrentlyReadingListCommand, Response<string>>,
        IRequestHandler<RemoveBookFromReadListCommand, Response<string>>,
        IRequestHandler<RemoveBookFromWantToReadListCommand, Response<string>>





    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IReaderService _readerService;

        public ReadersCommandHandler(IMapper mapper,
                                     IReaderService readerService,
                                     IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
            _readerService = readerService;
        }

        public async Task<Response<string>> Handle(AddBookToCurrentlyReadingListCommand request, CancellationToken cancellationToken)
        {
            var result = await _readerService.AddToCurrentlyReadingAsync(request.UserID, request.BookID);
            switch (result)
            {
                case "UserNotFound":
                    return Unauthorized<string>(_localizer[SharedResourceKeys.UserNotFound]);
                case "BookNotFound":
                    return Unauthorized<string>(_localizer[SharedResourceKeys.BookNotFound]);
                case "Success": return Success<string>(_localizer[SharedResourceKeys.Success]);

                default: return BadRequest<string>(result);
            }
        }

        public async Task<Response<string>> Handle(AddBookToReadListCommand request, CancellationToken cancellationToken)
        {
            var result = await _readerService.AddToReadAsync(request.UserID, request.BookID);
            switch (result)
            {
                case "UserNotFound":
                    return Unauthorized<string>(_localizer[SharedResourceKeys.UserNotFound]);
                case "BookNotFound":
                    return Unauthorized<string>(_localizer[SharedResourceKeys.BookNotFound]);
                case "Success": return Success<string>(_localizer[SharedResourceKeys.Success]);

                default: return BadRequest<string>(result);
            }
        }

        public async Task<Response<string>> Handle(AddBookToWantToReadListCommand request, CancellationToken cancellationToken)
        {
            var result = await _readerService.AddToWantToReadAsync(request.UserID, request.BookID);
            switch (result)
            {
                case "UserNotFound":
                    return Unauthorized<string>(_localizer[SharedResourceKeys.UserNotFound]);
                case "BookNotFound":
                    return Unauthorized<string>(_localizer[SharedResourceKeys.BookNotFound]);
                case "Success": return Success<string>(_localizer[SharedResourceKeys.Success]);

                default: return BadRequest<string>(result);
            }
        }

        public async Task<Response<string>> Handle(RemoveBookFromCurrentlyReadingListCommand request, CancellationToken cancellationToken)
        {
            var result = await _readerService.RemoveFromCurrentlyReadingAsync(request.UserID, request.BookID);
            switch (result)
            {
                case "UserNotFound":
                    return Unauthorized<string>(_localizer[SharedResourceKeys.UserNotFound]);
                case "BookNotFound":
                    return Unauthorized<string>(_localizer[SharedResourceKeys.BookNotFound]);
                case "Success": return Success<string>(_localizer[SharedResourceKeys.Success]);

                default: return BadRequest<string>(result);
            }
        }

        public async Task<Response<string>> Handle(RemoveBookFromReadListCommand request, CancellationToken cancellationToken)
        {
            var result = await _readerService.RemoveFromReadAsync(request.UserID, request.BookID);
            switch (result)
            {
                case "UserNotFound":
                    return Unauthorized<string>(_localizer[SharedResourceKeys.UserNotFound]);
                case "BookNotFound":
                    return Unauthorized<string>(_localizer[SharedResourceKeys.BookNotFound]);
                case "Success": return Success<string>(_localizer[SharedResourceKeys.Success]);

                default: return BadRequest<string>(result);
            }
        }

        public async Task<Response<string>> Handle(RemoveBookFromWantToReadListCommand request, CancellationToken cancellationToken)
        {
            var result = await _readerService.RemoveFromWantToReadListAsync(request.UserID, request.BookID);
            switch (result)
            {
                case "UserNotFound":
                    return Unauthorized<string>(_localizer[SharedResourceKeys.UserNotFound]);
                case "BookNotFound":
                    return Unauthorized<string>(_localizer[SharedResourceKeys.BookNotFound]);
                case "Success": return Success<string>(_localizer[SharedResourceKeys.Success]);

                default: return BadRequest<string>(result);
            }
        }
    }
}
