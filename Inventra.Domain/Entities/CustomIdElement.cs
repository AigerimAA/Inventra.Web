using System;
using System.Collections.Generic;
using System.Text;
using Inventra.Domain.Enums;

namespace Inventra.Domain.Entities
{
    public class CustomIdElement
    {
        public int Id { get; set; }
        public CustomIdElementType ElementType { get; set; } = CustomIdElementType.Fixed;
        public string? FormatString { get; set; }
        public string? FixedValue { get; set; }
        public int SortOrder { get; set; }

        public int FormatId { get; set; }
        public CustomIdFormat Format { get; set; } = null!;
    }
}
