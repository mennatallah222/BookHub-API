﻿using API.Core.Bases;
using ClassLibrary1.Data_ClassLibrary1.Core.Requests;
using MediatR;

namespace API.Core.Features.Authorization.Commands.Models
{
    public class UpdateUserClaimsCommand : UpdateUserClaimsRequest, IRequest<Response<string>>
    {
    }
}
