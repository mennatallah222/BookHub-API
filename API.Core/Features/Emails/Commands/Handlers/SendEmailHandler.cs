using API.Core.Bases;
using API.Core.Features.Emails.Commands.Models;
using API.Core.SharedResource;
using API.Service.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace API.Core.Features.Emails.Commands.Handlers
{
    public class SendEmailHandler : Response_Handler,
        IRequestHandler<SendEmailCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IEmailService _emailService;

        public SendEmailHandler(IMapper mapper,
                                     IEmailService emailService,
                                     IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _mapper = mapper;
            _localizer = localizer;
            _emailService = emailService;
        }
        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var response = await _emailService.SendEmail(request.Email, request.Message, null);
            if (response == "Success")
            {
                return Success<string>(response);
            }
            return BadRequest<string>(_localizer[SharedResourceKeys.FailedToSendEmail]);
        }
    }
}
