using MediatR;

namespace Inventra.Application.Likes.Commands
{
    public record ToggleLikeCommand(int ItemId, string UserId) : IRequest;
}
