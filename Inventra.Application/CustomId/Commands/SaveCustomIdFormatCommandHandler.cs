using Inventra.Application.Interfaces;
using Inventra.Domain.Entities;
using Inventra.Domain.Enums;
using Inventra.Domain.Interfaces;
using MediatR;

namespace Inventra.Application.CustomId.Commands
{
    public class SaveCustomIdFormatCommandHandler : IRequestHandler<SaveCustomIdFormatCommand>
    {
        private readonly ICustomIdRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public SaveCustomIdFormatCommandHandler(ICustomIdRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(SaveCustomIdFormatCommand request, CancellationToken cancellationToken)
        {
            var format = new CustomIdFormat
            {
                InventoryId = request.Format.InventoryId,
                UpdatedAt = DateTime.UtcNow,
                Elements = request.Format.Elements.Select((e, i) =>
                {
                    if (!Enum.TryParse<CustomIdElementType>(e.ElementType, out var elementType))
                        throw new ArgumentException($"Invalid element type: {e.ElementType}");
                    return new CustomIdElement
                    {
                        ElementType = elementType,
                        FormatString = e.FormatString,
                        FixedValue = e.FixedValue,
                        SortOrder = i
                    };
                }).ToList()
            };
            await _repository.SaveAsync(format, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
