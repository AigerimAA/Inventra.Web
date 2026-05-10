using Inventra.Application.DTOs;
using MediatR;

namespace Inventra.Application.Tags.Queries.GetTagCloud
{
    public record GetTagCloudQuery(int MaxTags) : IRequest<IEnumerable<TagCloudItemDto>>; 
}
