using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HypeLabs.Partner.Sdk;

/// <summary>
/// Registers <see cref="PartnerClient"/> in a dependency-injection container.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers a <see cref="PartnerClient"/> configured with the given <paramref name="configure"/> callback.
    /// The client is backed by <c>IHttpClientFactory</c>, so its handler is pooled and its lifetime managed for you.
    /// Inject <see cref="PartnerClient"/> anywhere afterwards.
    /// </summary>
    /// <example>
    /// <code>
    /// builder.Services.AddPartnerClient(options => options.ApiKey = config["Partner:ApiKey"]!);
    /// </code>
    /// </example>
    public static IServiceCollection AddPartnerClient(
        this IServiceCollection services,
        Action<PartnerClientOptions> configure)
    {
        services.AddOptions<PartnerClientOptions>()
            .Configure(configure)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services.AddPartnerClientCore();
    }

    /// <summary>
    /// Registers a <see cref="PartnerClient"/> whose options are bound from configuration — pass the
    /// <c>Partner</c> section (or any section) so <c>ApiKey</c> and <c>BaseUrl</c> come from your settings.
    /// </summary>
    /// <example>
    /// <code>
    /// builder.Services.AddPartnerClient(builder.Configuration.GetSection("Partner"));
    /// </code>
    /// </example>
    public static IServiceCollection AddPartnerClient(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<PartnerClientOptions>()
            .Bind(configuration)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services.AddPartnerClientCore();
    }

    private static IServiceCollection AddPartnerClientCore(this IServiceCollection services)
    {
        // A named HttpClient the factory owns; PartnerClient sends its requests through it.
        services.AddHttpClient(nameof(PartnerClient));

        services.AddSingleton(serviceProvider =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<PartnerClientOptions>>().Value;
            var httpClient = serviceProvider
                .GetRequiredService<IHttpClientFactory>()
                .CreateClient(nameof(PartnerClient));

            return new PartnerClient(options.ApiKey, httpClient, options.BaseUrl);
        });

        return services;
    }
}
