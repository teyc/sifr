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
            // Skip enums, value types, and Monetary (value object)
            if (type.IsEnum || !type.IsClass || type.Name == "Monetary") continue;

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
}