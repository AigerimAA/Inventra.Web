using MediatR;

namespace Inventra.Application.Tags.Queries.GetTagsByPrefix
{
    public record GetTagsByPrefixQuery(string Prefix, int MaxResult = 10)
        : IRequest<IEnumerable<string>>;
}
