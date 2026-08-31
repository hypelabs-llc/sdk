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
    /// Creates a client authenticated with the given API key, with an internally-managed <see cref="HttpClient"/>.
    /// Prefer <c>AddPartnerClient</c> in a DI-based app so the HTTP handler is pooled by the factory.
    /// </summary>
    /// <param name="apiKey">A Partner API key (e.g. <c>hl_live_…</c>).</param>
    public PartnerClient(string apiKey)
        : base(new HttpClientRequestAdapter(new ApiKeyAuthenticationProvider(NotEmpty(apiKey))))
    {
    }

    /// <summary>
    /// Creates a client that sends its requests through the supplied <see cref="HttpClient"/> — the constructor
    /// used by the DI factory, which owns the client's lifetime and handler pooling.
    /// </summary>
    /// <param name="apiKey">A Partner API key (e.g. <c>hl_live_…</c>).</param>
    /// <param name="httpClient">The HTTP client to send requests with, typically from <c>IHttpClientFactory</c>.</param>
    /// <param name="baseUrl">Optional API base URL; leave null to use the SDK default.</param>
    public PartnerClient(string apiKey, HttpClient httpClient, string? baseUrl = null)
        : base(new HttpClientRequestAdapter(
            new ApiKeyAuthenticationProvider(NotEmpty(apiKey)),
            httpClient: httpClient))
    {
        if (!string.IsNullOrWhiteSpace(baseUrl))
            RequestAdapter.BaseUrl = baseUrl;
    }

    private static string NotEmpty(string apiKey) => string.IsNullOrWhiteSpace(apiKey)
        ? throw new ArgumentException("An API key is required.", nameof(apiKey))
        : apiKey;
}
