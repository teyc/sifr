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
    public class Transaction
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        
        public Monetary Amount { get; set; } = new();
        
        public string Description { get; set; } = string.Empty;
        public Guid? AccountId { get; set; }
        public TransactionStatus Status { get; set; }
        public string Source { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Transaction() { }

        public Transaction(Guid id, DateTime date, Monetary amount, string description, Guid? accountId, TransactionStatus status, string source, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Description = description;
            AccountId = accountId;
            Status = status;
            Source = source;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
