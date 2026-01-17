using System.Text.Json.Serialization;

namespace Sifr.Shared.Models;

public record JournalEntry(
    Guid Id,
    DateTime Date,
    string Description,
    string Reference,
    Guid? TransactionId, // Link to bank transaction if applicable
    List<JournalEntryLine> Lines,
    JournalEntryStatus Status,
    string Source,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record JournalEntryLine(
    Guid Id,
    Guid AccountId,
    [property: JsonPropertyName("amount")] Monetary Amount,
    EntryType EntryType, // Debit or Credit
    string? Memo
);