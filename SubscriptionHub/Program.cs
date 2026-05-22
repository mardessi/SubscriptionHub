using SubscriptionHub.Application;
using SubscriptionHub.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplication();
// Une seule ligne pour toute l'infrastructure 🎯
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
