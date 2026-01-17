using System;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Sifr.Shared.Models;
using Xunit;

namespace Sifr.Shared.Tests;

public class ConventionTests
{
    [Fact]
    public void AllModels_ShouldFollowAccountingBestPractices()
    {
        // Arrange
        var assembly = Assembly.Load("Sifr.Shared");
        var modelTypes = assembly.GetTypes()
            .Where(t => t.Namespace == "Sifr.Shared.Models" && t.IsClass)
            .ToList();

        // Act & Assert
        foreach (var type in modelTypes)
        {
            // Skip enums, value types, Monetary (value object), and nested types
            if (type.IsEnum || !type.IsClass || type.Name == "Monetary" ||
                type.Namespace != "Sifr.Shared.Models" || type.IsNested ||
                type.Name.EndsWith("Line")) // Skip line item types
                continue;

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Check for required properties
            var hasId = properties.Any(p => p.Name == "Id" && p.PropertyType == typeof(Guid));
            Assert.True(hasId, $"Model {type.Name} must have a Guid Id property.");

            var hasCreatedAt = properties.Any(p => p.Name == "CreatedAt" && p.PropertyType == typeof(DateTime));
            Assert.True(hasCreatedAt, $"Model {type.Name} must have a DateTime CreatedAt property.");

            var hasUpdatedAt = properties.Any(p => p.Name == "UpdatedAt" && p.PropertyType == typeof(DateTime));
            Assert.True(hasUpdatedAt, $"Model {type.Name} must have a DateTime UpdatedAt property.");

            foreach (var prop in properties)
            {
                // No floating-point types (float, double) for precision
                Assert.False(prop.PropertyType == typeof(float) || prop.PropertyType == typeof(double),
                    $"Model {type.Name} has a floating-point property '{prop.Name}'. Use decimal or Monetary instead.");

                // No decimal properties except in Monetary
                if (prop.PropertyType == typeof(decimal))
                {
                    Assert.True(type.Name == "Monetary",
                        $"Model {type.Name} has a decimal property '{prop.Name}'. Use Monetary type instead.");
                }

                // Status properties should be enums, not strings
                if (prop.Name == "Status")
                {
                    Assert.True(prop.PropertyType.IsEnum,
                        $"Model {type.Name} Status property should be an enum, not {prop.PropertyType.Name}.");
                }

                // No separate Currency string properties, except in Monetary
                if (prop.Name == "Currency" && prop.PropertyType == typeof(string))
                {
                    Assert.True(type.Name == "Monetary",
                        $"Model {type.Name} has a separate 'Currency' property. Use Monetary type instead.");
                }
            }
        }
    }

    [Fact]
    public void Transaction_ShouldSerializeMonetaryValuesCorrectly()
    {
        // Arrange
        var monetary = new Monetary(AmountMinor: 12345, Scale: 2, Currency: "AUD");
        var transaction = new Transaction(
            Id: Guid.NewGuid(),
            Date: DateTime.Now,
            Amount: monetary,
            Description: "Test transaction",
            AccountId: null,
            Status: TransactionStatus.Pending,
            Source: "Bank",
            CreatedAt: DateTime.Now,
            UpdatedAt: DateTime.Now
        );

        // Act
        var json = JsonSerializer.Serialize(transaction);
        var jsonDocument = JsonDocument.Parse(json);
        var root = jsonDocument.RootElement;

        // Assert
        Assert.True(root.TryGetProperty("amount", out var amountObj));
        Assert.Equal(JsonValueKind.Object, amountObj.ValueKind);

        Assert.True(amountObj.TryGetProperty("amount_minor", out var amountMinor));
        Assert.Equal(12345, amountMinor.GetInt32());

        Assert.True(amountObj.TryGetProperty("scale", out var scale));
        Assert.Equal(2, scale.GetInt32());

        Assert.True(amountObj.TryGetProperty("currency", out var currency));
        Assert.Equal("AUD", currency.GetString());
    }

    [Fact]
    public void JournalEntry_ShouldFollowDoubleEntryBookkeepingRules()
    {
        // Arrange
        var debitLine = new JournalEntryLine(
            Id: Guid.NewGuid(),
            AccountId: Guid.NewGuid(),
            Amount: new Monetary(AmountMinor: 10000, Scale: 2, Currency: "USD"),
            EntryType: EntryType.Debit,
            Memo: "Debit entry"
        );

        var creditLine = new JournalEntryLine(
            Id: Guid.NewGuid(),
            AccountId: Guid.NewGuid(),
            Amount: new Monetary(AmountMinor: 10000, Scale: 2, Currency: "USD"),
            EntryType: EntryType.Credit,
            Memo: "Credit entry"
        );

        var journalEntry = new JournalEntry(
            Id: Guid.NewGuid(),
            Date: DateTime.Now,
            Description: "Test double-entry",
            Reference: "REF001",
            TransactionId: null,
            Lines: new List<JournalEntryLine> { debitLine, creditLine },
            Status: JournalEntryStatus.Draft,
            Source: "Test",
            CreatedAt: DateTime.Now,
            UpdatedAt: DateTime.Now
        );

        // Act
        var debits = journalEntry.Lines.Where(l => l.EntryType == EntryType.Debit).Sum(l => l.Amount.AmountMinor);
        var credits = journalEntry.Lines.Where(l => l.EntryType == EntryType.Credit).Sum(l => l.Amount.AmountMinor);

        // Assert
        Assert.True(debits == credits, "Debits must equal credits in double-entry bookkeeping.");
        Assert.True(journalEntry.Lines.Any(l => l.EntryType == EntryType.Debit), "Journal entry must have at least one debit.");
        Assert.True(journalEntry.Lines.Any(l => l.EntryType == EntryType.Credit), "Journal entry must have at least one credit.");
    }
}