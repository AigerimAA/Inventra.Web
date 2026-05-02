using Inventra.Domain.Entities;
using Inventra.Domain.Interfaces;
using MediatR;

namespace Inventra.Application.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<Category>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetAllAsync();
        }
    }
}
