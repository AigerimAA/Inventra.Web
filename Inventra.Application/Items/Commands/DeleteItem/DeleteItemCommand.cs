using MediatR;

namespace Inventra.Application.Items.Commands.DeleteItem
{
    public record DeleteItemCommand(int Id) : IRequest;
}
