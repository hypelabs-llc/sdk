using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;

namespace HypeLabs.Partner.Sdk;

/// <summary>
/// Authenticates every request with the Partner API's <c>X-Api-Key</c> header. Kiota calls this before each
/// outgoing request; the key is supplied once when the <see cref="PartnerClient"/> is constructed.
/// </summary>
internal sealed class ApiKeyAuthenticationProvider(string apiKey) : IAuthenticationProvider
{
    public Task AuthenticateRequestAsync(
        RequestInformation request,
        Dictionary<string, object>? additionalAuthenticationContext = null,
        CancellationToken cancellationToken = default)
    {
        request.Headers.TryAdd("X-Api-Key", apiKey);
        return Task.CompletedTask;
    }
}
