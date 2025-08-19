using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using OICT_Test.Helpers;
using OICT_Test.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "OICT Project",
        Version = "v1",
        Description = "API pro kontrolu stavu a platnosti karty veřejné dopravy"
    });

    // Support text/plain in Swagger UI
    c.MapType<string>(() => new OpenApiSchema { Type = "string" });
});
builder.Services.AddHealthChecks().AddCheck<CheckService>("ServiceHealthCheck");

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.Configure<AppSettingsWrapper>(
    builder.Configuration.GetSection("CardApi"));

builder.Services.AddHttpClient<ICardService, CardService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.MapGet("/Check", async (HealthCheckService healthCheckService) =>
{
    var result = await healthCheckService.CheckHealthAsync();

    if (result.Status == HealthStatus.Healthy)
    {
        return Results.Ok("OK");
    }
    else
    {
        return Results.Ok("Not OK");
    }
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
