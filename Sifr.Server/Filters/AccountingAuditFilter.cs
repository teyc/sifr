using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Sifr.Server.Services;

namespace Sifr.Server.Filters;

public class AccountingAuditFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // This runs before the action method
        var auditService = context.HttpContext.RequestServices.GetRequiredService<IAuditService>();
        var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
        var userName = context.HttpContext.User.Identity?.Name ?? "Anonymous";

        // Extract entity information from route data
        if (context.RouteData.Values.TryGetValue("id", out var idValue) && Guid.TryParse(idValue?.ToString(), out var entityId))
        {
            var controller = context.RouteData.Values["controller"]?.ToString();
            var action = context.RouteData.Values["action"]?.ToString();

            // Determine entity type from controller
            var entityType = controller switch
            {
                "Transactions" => "Transaction",
                "Accounts" => "Account",
                "Invoices" => "Invoice",
                "Bills" => "Bill",
                "Journal" => "JournalEntry",
                _ => "Unknown"
            };

            // Store audit context for use in OnActionExecuted
            context.HttpContext.Items["AuditContext"] = new AuditContext
            {
                UserId = userId,
                UserName = userName,
                EntityType = entityType,
                EntityId = entityId,
                Action = action ?? "Unknown",
                Method = context.HttpContext.Request.Method,
                Endpoint = context.HttpContext.Request.Path
            };
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // This runs after the action method
        var auditContext = context.HttpContext.Items["AuditContext"] as AuditContext;
        if (auditContext == null) return;

        var auditService = context.HttpContext.RequestServices.GetRequiredService<IAuditService>();

        // Determine the specific action type
        var actionType = context.HttpContext.Request.Method switch
        {
            "GET" => "Read",
            "POST" => "Create",
            "PUT" => "Update",
            "DELETE" => "Delete",
            _ => "Unknown"
        };

        // For create operations, try to extract the new entity ID from the result
        if (context.HttpContext.Request.Method == "POST" &&
            context.Result is CreatedAtActionResult createdResult &&
            createdResult.RouteValues?.TryGetValue("id", out var newId) == true &&
            Guid.TryParse(newId?.ToString(), out var newEntityId))
        {
            auditContext.EntityId = newEntityId;
        }

        // Log the operation asynchronously (fire and forget for performance)
        _ = Task.Run(() => auditService.LogAccountingOperationAsync(
            auditContext.UserId,
            auditContext.UserName,
            actionType,
            auditContext.EntityType,
            auditContext.EntityId,
            auditContext.Endpoint,
            auditContext.Method,
            ipAddress: context.HttpContext.Connection.RemoteIpAddress?.ToString(),
            userAgent: context.HttpContext.Request.Headers["User-Agent"].ToString(),
            responseStatusCode: context.HttpContext.Response.StatusCode
        ));
    }

    private class AuditContext
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string EntityType { get; set; } = string.Empty;
        public Guid EntityId { get; set; }
        public string Action { get; set; } = string.Empty;
        public string Method { get; set; } = string.Empty;
        public string Endpoint { get; set; } = string.Empty;
    }
}