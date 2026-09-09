using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SubscriptionHub.Api.Middleware;
using SubscriptionHub.Application;
using SubscriptionHub.Application.Common.Interfaces;
using SubscriptionHub.Domain.Entities;
using SubscriptionHub.Domain.Enums;
using SubscriptionHub.Infrastructure;
using SubscriptionHub.Infrastructure.Persistence;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplication();
// Une seule ligne pour toute l'infrastructure 🎯
builder.Services.AddInfrastructure(builder.Configuration);
// Après AddInfrastructure(builder.Configuration) :
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secret = jwtSettings["Secret"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secret!))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlingMiddleware>();
//app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();   // ← AVANT UseAuthorization
app.UseAuthorization();

app.MapControllers();

// Juste avant app.Run()
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();

    // 1. Applique les migrations automatiquement
    context.Database.Migrate();

    // 2. Seed tenant + user si base vide
    if (!context.Tenants.Any())
    {
        var tenant = new Tenant("SubscriptionHub", "subscriptionhub", "admin@subscriptionhub.com");
        context.Tenants.Add(tenant);
        await context.SaveChangesAsync();

        var passwordHasher = scope.ServiceProvider
            .GetRequiredService<IPasswordHasher>();

        var user = new User(
            tenant.Id,
            "admin@test.com",
            "Admin",
            "Test",
            UserRole.Admin,
            passwordHasher.Hash("Admin123!")
        );
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }
}

app.Run();
app.Run();
