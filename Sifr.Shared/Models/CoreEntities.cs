using System;
using System.Collections.Generic;

namespace Sifr.Shared.Models
{
    /// <summary>
    /// Base entity for all core models.
    /// </summary>
    public abstract record BaseEntity(
        /// <summary>
        /// Unique identifier for the entity.
        /// </summary>
        Guid Id,
        /// <summary>
        /// Timestamp when the entity was created.
        /// </summary>
        DateTime CreatedAt,
        /// <summary>
        /// Timestamp when the entity was last updated.
        /// </summary>
        DateTime UpdatedAt
    );

    /// <summary>
    /// Represents a company/tenant.
    /// </summary>
    public record Company(
        /// <summary>
        /// Unique identifier for the company.
        /// </summary>
        Guid Id,
        /// <summary>
        /// Name of the company.
        /// </summary>
        string Name,
        /// <summary>
        /// Contact email for the company.
        /// </summary>
        string? Email,
        /// <summary>
        /// Contact phone number for the company.
        /// </summary>
        string? Phone,
        /// <inheritdoc/>
        DateTime CreatedAt,
        /// <inheritdoc/>
        DateTime UpdatedAt
    ) : BaseEntity(Id, CreatedAt, UpdatedAt);

    /// <summary>
    /// Represents a line item on an invoice.
    /// </summary>
    public record InvoiceLine(
        /// <summary>
        /// Unique identifier for the invoice line.
        /// </summary>
        Guid Id,
        /// <summary>
        /// Description of the line item.
        /// </summary>
        string Description,
        /// <summary>
        /// Account associated with this line item.
        /// </summary>
        Guid AccountId,
        /// <summary>
        /// Quantity of items/services.
        /// </summary>
        int Quantity,
        /// <summary>
        /// Unit amount for the line item.
        /// </summary>
        Monetary UnitAmount,
        /// <summary>
        /// Tax code applied to this line item.
        /// </summary>
        Guid TaxCodeId,
        /// <inheritdoc/>
        DateTime CreatedAt,
        /// <inheritdoc/>
        DateTime UpdatedAt
    ) : BaseEntity(Id, CreatedAt, UpdatedAt);

    /// <summary>
    /// Represents a bill (accounts payable).
    /// </summary>
    public record Bill(
        /// <summary>
        /// Unique identifier for the bill.
        /// </summary>
        Guid Id,
        /// <summary>
        /// Supplier associated with the bill.
        /// </summary>
        Guid SupplierId,
        /// <summary>
        /// Date the bill was issued.
        /// </summary>
        DateTime Date,
        /// <summary>
        /// Due date for payment.
        /// </summary>
        DateTime DueDate,
        /// <summary>
        /// Total amount of the bill.
        /// </summary>
        Monetary Amount,
        /// <summary>
        /// Status of the bill (Draft, Approved, Paid).
        /// </summary>
        BillStatus Status,
        /// <summary>
        /// Line items on the bill.
        /// </summary>
        List<BillLine> Lines,
        /// <inheritdoc/>
        DateTime CreatedAt,
        /// <inheritdoc/>
        DateTime UpdatedAt
    ) : BaseEntity(Id, CreatedAt, UpdatedAt);

    /// <summary>
    /// Represents a line item on a bill.
    /// </summary>
    public record BillLine(
        /// <summary>
        /// Unique identifier for the bill line.
        /// </summary>
        Guid Id,
        /// <summary>
        /// Description of the line item.
        /// </summary>
        string Description,
        /// <summary>
        /// Account associated with this line item.
        /// </summary>
        Guid AccountId,
        /// <summary>
        /// Quantity of items/services.
        /// </summary>
        int Quantity,
        /// <summary>
        /// Unit amount for the line item.
        /// </summary>
        Monetary UnitAmount,
        /// <summary>
        /// Tax code applied to this line item.
        /// </summary>
        Guid TaxCodeId,
        /// <inheritdoc/>
        DateTime CreatedAt,
        /// <inheritdoc/>
        DateTime UpdatedAt
    ) : BaseEntity(Id, CreatedAt, UpdatedAt);

    /// <summary>
    /// Represents a tax code/rate.
    /// </summary>
    public record TaxCode(
        /// <summary>
        /// Unique identifier for the tax code.
        /// </summary>
        Guid Id,
        /// <summary>
        /// Code for the tax (e.g., GST10).
        /// </summary>
        string Code,
        /// <summary>
        /// Name of the tax code.
        /// </summary>
        string Name,
        /// <summary>
        /// Tax rate as a decimal (e.g., 0.10 for 10%).
        /// </summary>
        decimal Rate,
        /// <summary>
        /// Jurisdiction for the tax code.
        /// </summary>
        string Jurisdiction,
        /// <inheritdoc/>
        DateTime CreatedAt,
        /// <inheritdoc/>
        DateTime UpdatedAt
    ) : BaseEntity(Id, CreatedAt, UpdatedAt);

    /// <summary>
    /// Status of a bill in the workflow.
    /// </summary>
    public enum BillStatus
    {
        /// <summary>
        /// Bill is in draft state.
        /// </summary>
        Draft,
        /// <summary>
        /// Bill is approved for payment.
        /// </summary>
        Approved,
        /// <summary>
        /// Bill has been paid.
        /// </summary>
        Paid
    }
}
