using Sifr.Shared.Models;

namespace Sifr.Server.Services;

public interface IAccountingValidationService
{
    ValidationResult ValidateJournalEntry(JournalEntry entry);
    ValidationResult ValidateTransactionMatch(Transaction transaction, Guid debitAccountId, Guid creditAccountId);
}

public class AccountingValidationService : IAccountingValidationService
{
    public ValidationResult ValidateJournalEntry(JournalEntry entry)
    {
        var errors = new List<string>();

        // Must have at least one debit and one credit
        var debitLines = entry.Lines.Where(l => l.EntryType == EntryType.Debit).ToList();
        var creditLines = entry.Lines.Where(l => l.EntryType == EntryType.Credit).ToList();

        if (!debitLines.Any())
            errors.Add("Journal entry must have at least one debit line.");

        if (!creditLines.Any())
            errors.Add("Journal entry must have at least one credit line.");

        // Debits must equal credits (double-entry principle)
        var totalDebits = debitLines.Sum(l => l.Amount.AmountMinor / (decimal)Math.Pow(10, l.Amount.Scale));
        var totalCredits = creditLines.Sum(l => l.Amount.AmountMinor / (decimal)Math.Pow(10, l.Amount.Scale));

        if (Math.Abs(totalDebits - totalCredits) > 0.01m) // Allow for small rounding differences
            errors.Add($"Debits ({totalDebits}) must equal credits ({totalCredits}).");

        // All lines must have valid account IDs
        if (entry.Lines.Any(l => l.AccountId == Guid.Empty))
            errors.Add("All journal entry lines must have valid account IDs.");

        // Check for duplicate accounts (unusual but possible)
        var accountGroups = entry.Lines.GroupBy(l => l.AccountId);
        foreach (var group in accountGroups.Where(g => g.Count() > 1))
        {
            var accountLines = group.ToList();
            var hasBothTypes = accountLines.Any(l => l.EntryType == EntryType.Debit) &&
                              accountLines.Any(l => l.EntryType == EntryType.Credit);

            if (hasBothTypes)
                errors.Add($"Account {group.Key} cannot have both debit and credit entries in the same journal entry.");
        }

        return new ValidationResult(errors.Count == 0, errors);
    }

    public ValidationResult ValidateTransactionMatch(Transaction transaction, Guid debitAccountId, Guid creditAccountId)
    {
        var errors = new List<string>();

        if (debitAccountId == Guid.Empty)
            errors.Add("Debit account ID is required for transaction matching.");

        if (creditAccountId == Guid.Empty)
            errors.Add("Credit account ID is required for transaction matching.");

        if (debitAccountId == creditAccountId)
            errors.Add("Debit and credit accounts cannot be the same.");

        // Additional validation could check account types
        // (e.g., bank transactions typically debit expense accounts and credit bank accounts)

        return new ValidationResult(errors.Count == 0, errors);
    }
}

public record ValidationResult(bool IsValid, List<string> Errors);