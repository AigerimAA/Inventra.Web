using Inventra.Application.DTOs;
using MediatR;

namespace Inventra.Application.Access.Commands.AddAccess
{
    public record AddAccessCommand(int InventoryId, string TargetUserId) : IRequest<AccessUserDto>;
}
