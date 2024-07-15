using API.Core.Bases;
using ClassLibrary1.Data_ClassLibrary1.Core.DTOs;
using MediatR;

namespace API.Core.Features.Authorization.Commands.Models
{
    public class EditRoleCommand : EditRoleRequest, IRequest<Response<string>>
    {

    }
}
