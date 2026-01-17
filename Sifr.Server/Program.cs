using Sifr.Server.Middleware;
using Sifr.Server.Services;
using Sifr.Server.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

// Register audit service
builder.Services.AddScoped<IAuditService, AuditService>();

// Register accounting validation service
builder.Services.AddScoped<IAccountingValidationService, AccountingValidationService>();

// Add controllers with audit filter
builder.Services.AddControllers(options =>
{
    options.Filters.Add<AccountingAuditFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Add accounting audit middleware
app.UseMiddleware<AccountingAuditMiddleware>();

app.MapControllers();

app.Run();
