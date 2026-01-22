using System;
using System.Collections.Generic;

namespace Sifr.Shared.Models
{
    /// <summary>
    /// Base entity for all core models.
    /// </summary>
    public abstract record BaseEntity(
        Guid Id,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );

    /// <summary>
    /// Represents a company/tenant.
    /// </summary>
    public record Company(
        Guid Id,
        string Name,
        string? Email,
        string? Phone,
        DateTime CreatedAt,
        DateTime UpdatedAt
    ) : BaseEntity(Id, CreatedAt, UpdatedAt);

    /// <summary>
    /// Represents a line item on an invoice.
    /// </summary>
    public record InvoiceLine(
        Guid Id,
        string Description,
        Guid AccountId,
        int Quantity,
        Monetary UnitAmount,
        Guid TaxCodeId,
        DateTime CreatedAt,
        DateTime UpdatedAt
    ) : BaseEntity(Id, CreatedAt, UpdatedAt);

    /// <summary>
    /// Represents a bill (accounts payable).
    /// </summary>
    public record Bill(
        Guid Id,
        Guid SupplierId,
        DateTime Date,
        DateTime DueDate,
        Monetary Amount,
        BillStatus Status,
        List<BillLine> Lines,
        DateTime CreatedAt,
        DateTime UpdatedAt
    ) : BaseEntity(Id, CreatedAt, UpdatedAt);

    /// <summary>
    /// Represents a line item on a bill.
    /// </summary>
    public record BillLine(
        Guid Id,
        string Description,
        Guid AccountId,
        int Quantity,
        Monetary UnitAmount,
        Guid TaxCodeId,
        DateTime CreatedAt,
        DateTime UpdatedAt
    ) : BaseEntity(Id, CreatedAt, UpdatedAt);

    /// <summary>
    /// Represents a tax code/rate.
    /// </summary>
    public record TaxCode(
        Guid Id,
        string Code,
        string Name,
        decimal Rate,
        string Jurisdiction,
        DateTime CreatedAt,
        DateTime UpdatedAt
    ) : BaseEntity(Id, CreatedAt, UpdatedAt);

    public enum BillStatus
    {
        Draft,
        Approved,
        Paid
    }
}
