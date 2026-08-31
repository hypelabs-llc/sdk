using HypeLabs.Partner.Sdk.Generated;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace HypeLabs.Partner.Sdk;

/// <summary>
/// The entry point to the HypeLabs Partner API. Construct one with your API key and use the fluent request
/// builders it inherits from the generated client — <c>client.Products.GetAsync()</c>,
/// <c>client.Customers.PostAsync(...)</c>, and so on.
///
/// <code>
/// var client = new PartnerClient("hl_live_...");
/// var products = await client.Products.GetAsync();
/// </code>
///
/// The Kiota request adapter and the <c>X-Api-Key</c> authentication are wired up internally, so callers never
/// touch the generated <see cref="PartnerApiClient"/> plumbing directly.
/// </summary>
public sealed class PartnerClient : PartnerApiClient
{
    /// <summary>
    /// Creates a client authenticated with the given API key.
    /// </summary>
    /// <param name="apiKey">A Partner API key (e.g. <c>hl_live_…</c>).</param>
    public PartnerClient(string apiKey)
        : base(new HttpClientRequestAdapter(new ApiKeyAuthenticationProvider(NotEmpty(apiKey))))
    {
    }

    private static string NotEmpty(string apiKey) => string.IsNullOrWhiteSpace(apiKey)
        ? throw new ArgumentException("An API key is required.", nameof(apiKey))
        : apiKey;
}
