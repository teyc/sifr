using Microsoft.Extensions.Logging;
using Sifr.Shared.Models;

namespace Sifr.Server.Services;

public interface IAuditService
{
    Task LogAccountingOperationAsync(
        string userId,
        string userName,
        string action,
        string entityType,
        Guid entityId,
        string endpoint,
        string method,
        string? requestBody = null,
        string? responseBody = null,
        int? responseStatusCode = null,
        string? ipAddress = null,
        string? userAgent = null);
}

public class AuditService : IAuditService
{
    private readonly ILogger<AuditService> _logger;
    private readonly List<AuditLogEntry> _auditStore = new(); // In production, use a proper database

    public AuditService(ILogger<AuditService> logger)
    {
        _logger = logger;
    }

    public async Task LogAccountingOperationAsync(
        string userId,
        string userName,
        string action,
        string entityType,
        Guid entityId,
        string endpoint,
        string method,
        string? requestBody = null,
        string? responseBody = null,
        int? responseStatusCode = null,
        string? ipAddress = null,
        string? userAgent = null)
    {
        var auditEntry = new AuditLogEntry(
            Id: Guid.NewGuid(),
            Timestamp: DateTime.UtcNow,
            UserId: userId,
            UserName: userName,
            Action: action,
            EntityType: entityType,
            EntityId: entityId,
            Endpoint: endpoint,
            Method: method,
            RequestBody: requestBody,
            ResponseBody: responseBody,
            ResponseStatusCode: responseStatusCode ?? 0,
            IpAddress: ipAddress,
            UserAgent: userAgent,
            CreatedAt: DateTime.UtcNow,
            UpdatedAt: DateTime.UtcNow
        );

        // Store in memory for demo (use database in production)
        _auditStore.Add(auditEntry);

        // Structured logging for compliance
        _logger.LogInformation(
            "AUDIT: {Timestamp} | User: {UserId} ({UserName}) | Action: {Action} | Entity: {EntityType}/{EntityId} | Endpoint: {Method} {Endpoint} | Status: {StatusCode}",
            auditEntry.Timestamp,
            auditEntry.UserId,
            auditEntry.UserName,
            auditEntry.Action,
            auditEntry.EntityType,
            auditEntry.EntityId,
            auditEntry.Method,
            auditEntry.Endpoint,
            auditEntry.ResponseStatusCode);

        // Log full details at debug level for troubleshooting
        _logger.LogDebug(
            "AUDIT DETAILS: RequestBody={RequestBody}, ResponseBody={ResponseBody}, IP={IpAddress}, UserAgent={UserAgent}",
            auditEntry.RequestBody,
            auditEntry.ResponseBody,
            auditEntry.IpAddress,
            auditEntry.UserAgent);
    }

    // Method to retrieve audit logs (for compliance reporting)
    public IEnumerable<AuditLogEntry> GetAuditLogs(
        DateTime? startDate = null,
        DateTime? endDate = null,
        string? userId = null,
        string? entityType = null)
    {
        var query = _auditStore.AsQueryable();

        if (startDate.HasValue)
            query = query.Where(x => x.Timestamp >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(x => x.Timestamp <= endDate.Value);

        if (!string.IsNullOrEmpty(userId))
            query = query.Where(x => x.UserId == userId);

        if (!string.IsNullOrEmpty(entityType))
            query = query.Where(x => x.EntityType == entityType);

        return query.OrderByDescending(x => x.Timestamp);
    }
}