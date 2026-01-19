using System;
using System.Text.Json.Serialization;

namespace Sifr.Shared.Models
{
    /// <summary>
    /// Represents a financial transaction, such as a bank line item or manual journal.
    /// </summary>
    /// <param name="Id">Unique identifier for the transaction.</param>
    /// <param name="Date">Date the transaction occurred.</param>
    /// <param name="Amount">Monetary value of the transaction.</param>
    /// <param name="Description">User-facing description of the transaction.</param>
    /// <param name="AccountId">The ledger account this transaction is assigned to.</param>
    /// <param name="Status">Current reconciliation status (e.g., Pending, Matched).</param>
    /// <param name="Source">Origin of the transaction (e.g., "BankFeed", "Manual").</param>
    /// <param name="CreatedAt">Timestamp of creation.</param>
    /// <param name="UpdatedAt">Timestamp of last update.</param>
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
