using System.Text.Json.Serialization;

namespace Sifr.Shared.Models;

public record AuditLogEntry(
    Guid Id,
    DateTime Timestamp,
    string UserId,
    string UserName,
    string Action,
    string EntityType,
    Guid EntityId,
    string Endpoint,
    string Method,
    string? RequestBody,
    string? ResponseBody,
    int ResponseStatusCode,
    string? IpAddress,
    string? UserAgent,
    DateTime CreatedAt,
    DateTime UpdatedAt
);