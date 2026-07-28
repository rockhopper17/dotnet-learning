using Azure.Identity;
using inventory.Api.Data;
using inventory.Api.Features.Items;
using inventory.Api.Shared.Cors;
using inventory.Api.Shared.ErrorHandling;
using inventory.Api.Shared.OpenApi;
using inventory.Api.Shared.Authentication;
using Microsoft.AspNetCore.HttpLogging;
using inventory.Api.Features.Categories;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddProblemDetails()
                .AddExceptionHandler<GlobalExceptionHandler>();

var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions
{
    ManagedIdentityClientId = builder.Configuration["AZURE_CLIENT_ID"]
});

builder.AddinventoryNpgsql<inventoryContext>("inventoryDB", credential);

// Configure authentication options with validation
builder.Services.AddOptions<AuthOptions>()
                .Bind(builder.Configuration.GetSection(AuthOptions.SectionName))
                .ValidateDataAnnotations()
                .ValidateOnStart();

// Register the JWT Bearer options configurator first
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

// Then add the authentication services
builder.Services.AddAuthentication()
                .AddJwtBearer();

builder.Services.AddAuthorizationBuilder();

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.RequestMethod |
                            HttpLoggingFields.RequestPath |
                            HttpLoggingFields.ResponseStatusCode |
                            HttpLoggingFields.Duration;
    options.CombineLogs = true;
});

builder.AddinventoryOpenApi();

builder.AddinventoryCors();

builder.Services.AddValidation();

var app = builder.Build();

app.UseCors();

app.MapDefaultEndpoints();
app.MapItems();
app.MapCategories();

app.UseHttpLogging();

if (app.Environment.IsDevelopment())
{
    app.UseinventorySwaggerUI();
}
else
{
    app.UseExceptionHandler();
}

app.UseStatusCodePages();

await app.MigrateDbAsync();

app.Run();