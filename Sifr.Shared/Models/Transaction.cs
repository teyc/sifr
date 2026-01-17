using System;

namespace Sifr.Shared.Models
{
    public record Transaction(
        Guid Id,
        DateTime Date,
        decimal Amount,
        string Currency,
        string Description,
        Guid? AccountId,
        string Status,
        string Source,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}
