using MediatR;

namespace Inventra.Application.Access.Commands.RemoveAccess
{
    public record RemoveAccessCommand(int InventoryId, string TargetUserId) : IRequest;
}
