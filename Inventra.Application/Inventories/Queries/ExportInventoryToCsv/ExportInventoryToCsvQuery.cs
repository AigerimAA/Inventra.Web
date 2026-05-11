using MediatR;

namespace Inventra.Application.Inventories.Queries.ExportInventoryToCsv
{
    public record ExportInventoryToCsvQuery(int InventoryId) : IRequest<ExportResult>;

    public record ExportResult(byte[] Content, string FileName, string ContentType);
}
