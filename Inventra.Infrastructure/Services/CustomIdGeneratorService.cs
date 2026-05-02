using Inventra.Application.Interfaces;
using Inventra.Domain.Enums;
using Inventra.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Inventra.Infrastructure.Services
{
    public class CustomIdGeneratorService : ICustomIdGenerator
    {
        private readonly AppDbContext _context;

        public CustomIdGeneratorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateAsync(int inventoryId, CancellationToken cancellationToken)
        {
            var format = await _context.CustomIdFormats
                .Include(f => f.Elements.OrderBy(e => e.SortOrder))
                .FirstOrDefaultAsync(f => f.InventoryId == inventoryId, cancellationToken);

            if (format == null || !format.Elements.Any())
                return Guid.NewGuid().ToString("N")[..12].ToUpper();

            var sb = new StringBuilder();

            foreach (var element in format.Elements)
            {
                sb.Append(BuildElement(element, inventoryId));
            }
            return sb.ToString();
        }
        private string BuildElement(Inventra.Domain.Entities.CustomIdElement element, int inventoryId)
        {
            return element.ElementType switch
            {
                CustomIdElementType.Fixed =>
                    element.FixedValue ?? string.Empty,

                CustomIdElementType.DateTime =>
                    DateTime.UtcNow.ToString(
                        string.IsNullOrEmpty(element.FormatString) ? "yyyy" : element.FormatString),

                CustomIdElementType.Guid =>
                    Guid.NewGuid().ToString("N").ToUpper(),

                CustomIdElementType.Random6Digits =>
                    FormatNumber(Random.Shared.Next(0, 999999), element.FormatString, 6),

                CustomIdElementType.Random9Digits =>
                    FormatNumber(Random.Shared.Next(0, 999999999), element.FormatString, 9),

                CustomIdElementType.Random20 =>
                    FormatHex(Random.Shared.Next(0, 0xFFFFF), element.FormatString, 5),

                CustomIdElementType.Random32 =>
                    FormatHex((int)(Random.Shared.NextInt64(0, 0xFFFFFFFFL)),
                        element.FormatString, 8),

                CustomIdElementType.Sequence =>
                    FormatSequence(inventoryId, element.FormatString).GetAwaiter().GetResult(),

                _ => string.Empty
            };
        }

        private static string FormatNumber(int value, string? formatString, int defaultPadding)
        {
            if (string.IsNullOrEmpty(formatString))
                return value.ToString().PadLeft(defaultPadding, '0');

            if (formatString.StartsWith('D') &&
                int.TryParse(formatString[1..], out var width))
                return value.ToString().PadLeft(width, '0');

            return value.ToString();
        }

        private static string FormatHex(int value, string? formatString, int defaultWidth)
        {
            if (string.IsNullOrEmpty(formatString))
                return value.ToString("X").PadLeft(defaultWidth, '0');

            if (formatString.StartsWith('X') &&
                int.TryParse(formatString[1..], out var width))
                return value.ToString("X").PadLeft(width, '0');

            return value.ToString("X");
        }
        private async Task<string> FormatSequence(int inventoryId, string? formatString)
        {
            while (true)
            {
                var seq = await _context.InventorySequence
                    .FirstOrDefaultAsync(s => s.InventoryId == inventoryId);

                if (seq == null)
                {
                    seq = new Inventra.Domain.Entities.InventorySequence
                    {
                        InventoryId = inventoryId,
                        CurrentValue = 1
                    };
                    _context.InventorySequence.Add(seq);

                    try
                    {
                        await _context.SaveChangesAsync();
                        return FormatSequenceValue(1, formatString);
                    }
                    catch (DbUpdateException)
                    {
                        _context.Entry(seq).State = EntityState.Detached;
                        continue;
                    }
                }

                var oldValue = seq.CurrentValue;
                seq.CurrentValue = oldValue + 1;

                try
                {
                    await _context.SaveChangesAsync();
                    return FormatSequenceValue(seq.CurrentValue, formatString);
                }
                catch (DbUpdateConcurrencyException)
                {
                    await _context.Entry(seq).ReloadAsync();
                }
            }
        }

        private static string FormatSequenceValue(int value, string? formatString)
        {
            if (string.IsNullOrEmpty(formatString))
                return value.ToString();

            if (formatString.StartsWith('D') &&
                int.TryParse(formatString[1..], out var width))
                return value.ToString().PadLeft(width, '0');

            return value.ToString();
        }
    }
}
