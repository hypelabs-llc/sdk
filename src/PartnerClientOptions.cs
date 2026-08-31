using System.ComponentModel.DataAnnotations;

namespace HypeLabs.Partner.Sdk;

/// <summary>
/// Configuration for the Partner API client, bound from configuration (e.g. a <c>Partner</c> section) or set
/// inline when calling <c>AddPartnerClient</c>.
/// </summary>
public sealed class PartnerClientOptions
{
    /// <summary>The Partner API key used to authenticate every request (e.g. <c>hl_live_…</c>). Required.</summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "A Partner API key is required.")]
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// The API base URL. Leave null to use the SDK's default (<c>https://connect.hypelabs.network</c>); override
    /// only to point at a staging or local instance.
    /// </summary>
    public string? BaseUrl { get; set; }
}
