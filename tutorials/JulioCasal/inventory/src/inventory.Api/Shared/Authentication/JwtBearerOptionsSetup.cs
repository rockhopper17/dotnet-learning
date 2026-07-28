using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace inventory.Api.Shared.Authentication;

/// <summary>
/// Configures JWT Bearer options using AuthOptions.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="JwtBearerOptionsSetup"/> class.
/// </remarks>
/// <param name="authOptions">The authentication options.</param>
public class JwtBearerOptionsSetup(IOptions<AuthOptions> authOptions) : IConfigureNamedOptions<JwtBearerOptions>
{
    /// <summary>
    /// Configures the JWT Bearer options for the specified scheme.
    /// </summary>
    /// <param name="name">The name of the scheme being configured.</param>
    /// <param name="options">The JWT Bearer options to configure.</param>
    public void Configure(string? name, JwtBearerOptions options)
    {
        // Configure for the default JWT Bearer scheme or any named scheme
        if (name == JwtBearerDefaults.AuthenticationScheme || string.IsNullOrEmpty(name))
        {
            Configure(options);
        }
    }

    /// <summary>
    /// Configures the JWT Bearer options.
    /// </summary>
    /// <param name="options">The JWT Bearer options to configure.</param>
    public void Configure(JwtBearerOptions options)
    {
        options.Authority = authOptions.Value.Authority;
        options.Audience = authOptions.Value.Audience;
        options.MapInboundClaims = false;
        options.RequireHttpsMetadata = false;
    }
}
