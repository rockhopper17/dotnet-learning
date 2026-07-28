using System.ComponentModel.DataAnnotations;

namespace inventory.Api.Shared.Authentication;

/// <summary>
/// Configuration options for authentication settings.
/// </summary>
public class AuthOptions
{
    /// <summary>
    /// The configuration section name for authentication options.
    /// </summary>
    public const string SectionName = "Auth";

    /// <summary>
    /// Gets or sets the JWT authority URL.
    /// </summary>
    [Required]
    public string Authority { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the JWT audience.
    /// </summary>
    [Required]
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the API scope.
    /// </summary>
    [Required]
    public string ApiScope { get; set; } = string.Empty;
}
