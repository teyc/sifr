using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Sifr.Shared.Models
{
    public record Invoice(
        Guid Id,
        Guid CustomerId,
        DateTime Date,
        DateTime DueDate,
        [property: JsonPropertyName("amount")] Monetary Amount,
        InvoiceStatus Status,
        List<object> Lines,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}
