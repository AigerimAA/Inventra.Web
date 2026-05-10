using Inventra.Application.DTOs;
using MediatR;

namespace Inventra.Application.Search.Queries
{
    public record SearchQuery(string Q) : IRequest<SearchResultDto>;    
}
