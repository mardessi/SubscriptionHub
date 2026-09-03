using Microsoft.AspNetCore.Authentication.JwtBearer;
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

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();

    if (!context.Users.Any())
    {
        var passwordHasher = scope.ServiceProvider
            .GetRequiredService<IPasswordHasher>();

        var tenantId = context.Tenants.First().Id;

        var user = new User(
            tenantId,
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
