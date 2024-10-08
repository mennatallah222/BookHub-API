﻿using API.Core.Bases;
using ClassLibrary1.Data_ClassLibrary1.Core.Responses;
using MediatR;

namespace API.Core.Features.Authorization.Queries.Models
{
    public class ManageUserRoleQuery : IRequest<Response<ManageUserRoleResponse>>
    {
        public int UserId { get; set; }
    }
}
