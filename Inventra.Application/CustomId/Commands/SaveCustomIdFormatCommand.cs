using Inventra.Application.DTOs;
using MediatR;

namespace Inventra.Application.CustomId.Commands
{
    public record SaveCustomIdFormatCommand(CustomIdFormatDto Format) : IRequest;
}
