using System;
using System.Collections.Generic;

namespace Sifr.Shared.Models
{
    public record Invoice(
        Guid Id,
        Guid CustomerId,
        DateTime Date,
        DateTime DueDate,
        decimal Amount,
        string Currency,
        string Status,
        List<object> Lines
    );
}
