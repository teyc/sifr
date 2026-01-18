using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Sifr.Shared.Models;

namespace Sifr.Server.Middleware;

public class AccountingAuditMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AccountingAuditMiddleware> _logger;

    public AccountingAuditMiddleware(RequestDelegate next, ILogger<AccountingAuditMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
        var userName = context.User.Identity?.Name ?? "Anonymous";

        // Log API request for accounting operations
        if (IsAccountingEndpoint(request.Path))
        {
            _logger.LogInformation(
                "Accounting API Request: {Method} {Path} by User: {UserId} ({UserName}) at {Timestamp}",
                request.Method,
                request.Path,
                userId,
                userName,
                DateTime.UtcNow);

            // Capture request body for POST/PUT operations
            if (request.Method == "POST" || request.Method == "PUT")
            {
                request.EnableBuffering();
                var body = await new StreamReader(request.Body).ReadToEndAsync();
                request.Body.Position = 0;

                _logger.LogInformation(
                    "Accounting API Request Body: {Body}",
                    body);
            }
        }

        // Capture original response
        var originalResponseBody = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;
        try
        {
            await _next(context);

            // Log response for accounting operations
            if (IsAccountingEndpoint(request.Path))
            {
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                _logger.LogInformation(
                    "Accounting API Response: Status {StatusCode}, Body: {ResponseBody}",
                    context.Response.StatusCode,
                    responseText);
            }

            // Only copy response body if not 204 No Content
            if (context.Response.StatusCode != StatusCodes.Status204NoContent)
            {
                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalResponseBody);
            }
        }
        finally
        {
            context.Response.Body = originalResponseBody;
        }
    }

    private bool IsAccountingEndpoint(PathString path)
    {
        return path.StartsWithSegments("/api/transactions", StringComparison.OrdinalIgnoreCase) ||
               path.StartsWithSegments("/api/accounts", StringComparison.OrdinalIgnoreCase) ||
               path.StartsWithSegments("/api/invoices", StringComparison.OrdinalIgnoreCase) ||
               path.StartsWithSegments("/api/bills", StringComparison.OrdinalIgnoreCase) ||
               path.StartsWithSegments("/api/journal", StringComparison.OrdinalIgnoreCase);
    }
}