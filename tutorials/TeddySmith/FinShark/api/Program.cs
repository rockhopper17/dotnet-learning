using FinShark.api.Data;
using FinShark.api.Interfaces;
using FinShark.api.Repository;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    var baseConnection = builder.Configuration.GetConnectionString("BaseSqlServer");
    var dbName = builder.Configuration["DatabaseName"];
    opt.UseSqlServer($"{baseConnection};Database={dbName}");
});

builder.Services.AddControllers();

builder.Services.AddScoped<IStockRepository, StockRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();