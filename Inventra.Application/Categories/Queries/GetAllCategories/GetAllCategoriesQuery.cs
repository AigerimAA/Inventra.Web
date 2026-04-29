using Inventra.Domain.Entities;
using MediatR;

namespace Inventra.Application.Categories.Queries.GetAllCategories
{
    public record GetAllCategoriesQuery : IRequest<IEnumerable<Category>>;
}
