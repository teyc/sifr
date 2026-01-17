using System;
using System.Text.Json.Serialization;

namespace Sifr.Shared.Models
{
    public record Transaction(
        Guid Id,
        DateTime Date,
        [property: JsonPropertyName("amount")] Monetary Amount,
        string Description,
        Guid? AccountId,
        TransactionStatus Status,
        string Source,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}
