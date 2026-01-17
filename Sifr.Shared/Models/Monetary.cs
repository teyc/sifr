using System.Text.Json.Serialization;

namespace Sifr.Shared.Models
{
    public record Monetary(
        [property: JsonPropertyName("amount_minor")] int AmountMinor,
        [property: JsonPropertyName("scale")] int Scale,
        [property: JsonPropertyName("currency")] string Currency
    );
}