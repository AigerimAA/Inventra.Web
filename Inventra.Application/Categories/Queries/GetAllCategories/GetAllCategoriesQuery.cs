using Inventra.Application.DTOs;
using MediatR;

namespace Inventra.Application.Categories.Queries.GetAllCategories
{
    public record GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDto>>;
}
