using Sifr.Server.Middleware;
using Sifr.Server.Services;
using Sifr.Server.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sifr.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

// Register audit service
builder.Services.AddScoped<IAuditService, AuditService>();

// Register accounting validation service
builder.Services.AddScoped<IAccountingValidationService, AccountingValidationService>();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Add authentication and authorization
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

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

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseBlazorFrameworkFiles();
app.UseRouting();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
