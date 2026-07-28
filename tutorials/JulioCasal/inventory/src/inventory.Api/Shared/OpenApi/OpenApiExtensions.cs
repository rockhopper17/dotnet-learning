using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Microsoft.AspNetCore.OpenApi;
using inventory.Api.Shared.Authentication;

namespace inventory.Api.Shared.OpenApi;

public static class OpenApiExtensions
{
    public static IHostApplicationBuilder AddinventoryOpenApi(this IHostApplicationBuilder builder)
    {
        builder.Services.AddOpenApi(options =>
        {
            // Register document transformer that adds OAuth2 scheme + per-operation requirements (per official docs pattern)
            options.AddDocumentTransformer<OAuthSecurityDocumentTransformer>();
        });

        return builder;
    }

    public static WebApplication UseinventorySwaggerUI(this WebApplication app)
    {
        app.MapOpenApi();

        var authOptions = app.Services.GetRequiredService<IOptions<AuthOptions>>().Value;

        var swaggerUiClientId = app.Configuration["SWAGGERUI_CLIENTID"]
                                ?? throw new InvalidOperationException("SWAGGERUI_CLIENTID is not configured");

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json", "inventory API v1");

            options.OAuthClientId(swaggerUiClientId);

            options.OAuthUsePkce();
            options.OAuthScopes(authOptions.ApiScope);

            options.EnablePersistAuthorization();
        });

        return app;
    }
}

// Document transformer that adds OAuth2 Authorization Code (PKCE) scheme and applies it to all operations.
// Based on official docs pattern: add scheme at document level then loop operations to append security requirements.
internal sealed class OAuthSecurityDocumentTransformer(IOptions<AuthOptions> authOptions) : IOpenApiDocumentTransformer
{
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        document.Info ??= new OpenApiInfo();
        document.Info.Title = "inventory API";

        var opts = authOptions.Value;

        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

        // OAuth2 Authorization Code with PKCE flow for Keycloak
        document.Components.SecuritySchemes["oauth2"] = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Description = "Keycloak OAuth2 Authorization Code Flow (PKCE)",
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri(opts.Authority + "/protocol/openid-connect/auth"),
                    TokenUrl = new Uri(opts.Authority + "/protocol/openid-connect/token"),
                    Scopes = new Dictionary<string, string>
                    {
                        [opts.ApiScope] = "Access to inventory protected endpoints"
                    }
                }
            }
        };

        // Apply security requirement to every operation so Swagger UI sends the bearer token after Authorize.
        if (document.Paths is not null)
        {
            foreach (var path in document.Paths.Values)
            {
                if (path.Operations is null) continue;
                foreach (var kvp in path.Operations)
                {
                    var operation = kvp.Value;
                    operation.Security ??= [];
                    operation.Security.Add(new OpenApiSecurityRequirement
                    {
                        [new OpenApiSecuritySchemeReference("oauth2", document)] = [opts.ApiScope]
                    });
                }
            }
        }

        return Task.CompletedTask;
    }
}