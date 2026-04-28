using Inventra.Application.DTOs;
using MediatR;

namespace Inventra.Application.Items.Queries.GetItemById
{
    public record GetItemByIdQuery(int Id) : IRequest<ItemDto>;
}
