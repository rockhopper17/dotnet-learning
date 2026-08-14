using Microsoft.EntityFrameworkCore;
using LSC.SmartCertify.Domain.Entities;
using LSC.SmartCertify.Infrastructure;
using Scalar.AspNetCore;
using LSC.SmartCertify.Application;
using LSC.SmartCertify.Application.Interfaces.Courses;
using LSC.SmartCertify.Application.Services;
using LSC.SmartCertify.API.Filters;
using FluentValidation;
using LSC.SmartCertify.Application.DTOValidations;

// namespace LSC.SmartCertify.API;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<SmartCertifyContext>(options =>
// {
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"),
//         providerOptions => providerOptions.EnableRetryOnFailure());
// });
builder.Services.AddDbContext<SmartCertifyContext>(opt =>
    {
        var baseConnection = builder.Configuration.GetConnectionString("BaseSqlServer"); // set w user-secrets, not in app settings
        var dbName = builder.Configuration["DatabaseName"];
        opt.UseSqlServer($"{baseConnection};Database={dbName};TrustServerCertificate=True;");
    });


// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
}).ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

builder.Services.AddValidatorsFromAssemblyContaining<CreateCourseValidator>();

builder.Services.AddScoped<ICourseRepository, CourseRepositry>();
builder.Services.AddScoped<ICourseService, CourseService>();

// don't do this, esp not in production, only if getting access http/https errors or something
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("default", policy =>
//     {
//         policy.AllowAnyOrigin()
//             .AllowAnyHeader()
//             .AllowAnyMethod();
//     });
// });

var app = builder.Build();

// app.UseCors("default");

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
