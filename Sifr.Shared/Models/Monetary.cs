using System.Text.Json.Serialization;

namespace Sifr.Shared.Models
{
    public class Monetary
    {
        public int AmountMinor { get; set; }

        public int Scale { get; set; }

        public string Currency { get; set; } = string.Empty;

        public Monetary() { }

        public Monetary(int amountMinor, int scale, string currency)
        {
            AmountMinor = amountMinor;
            Scale = scale;
            Currency = currency;
        }
    }
}