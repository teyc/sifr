using System;

namespace Sifr.Shared.Models
{
    /// <summary>
    /// Represents a ledger account in the Chart of Accounts.
    /// </summary>
    /// <param name="Id">Unique identifier for the account.</param>
    /// <param name="Code">User-facing code for the account (e.g., "200").</param>
    /// <param name="Name">Name of the account (e.g., "Sales").</param>
    /// <param name="Type">Category of the account (e.g., "Revenue", "Expense").</param>
    /// <param name="TaxType">Default tax treatment for transactions on this account (e.g., "OUTPUT", "INPUT", "NONE").</param>
    /// <param name="ParentId">Optional parent account ID for hierarchical grouping.</param>
    /// <param name="CreatedAt">Timestamp of creation.</param>
    /// <param name="UpdatedAt">Timestamp of last update.</param>
    public record Account(
        Guid Id,
        string Code,
        string Name,
        string Type,
        string TaxType,
        Guid? ParentId,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}
