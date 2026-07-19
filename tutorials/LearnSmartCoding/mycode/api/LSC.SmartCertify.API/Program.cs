using Microsoft.EntityFrameworkCore;
using LSC.SmartCertify.Domain.Entities;
using LSC.SmartCertify.Infrastructure;
using Scalar.AspNetCore;

// namespace LSC.SmartCertify.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SmartCertifyContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"),
        providerOptions => providerOptions.EnableRetryOnFailure());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("My API");
        options.WithTheme(ScalarTheme.BluePlanet);
        // options.WithSidebar(false);
        options.HideSidebar();
    });

    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "openapi/v1.json";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
