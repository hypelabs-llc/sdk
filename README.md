<p align="center">
  <img src="https://i.imgur.com/NtKvmk2.png" height="100" alt="HypeLabs" />
</p>
<h3 align="center">
  HypeLabs Partner SDK
</h3>
<p align="center">
  The C# client for the HypeLabs Partner API, for internal .NET services. 🚀
</p>
<p align="center">
  <a href="https://connect.hypelabs.network"><img src="https://img.shields.io/badge/API-connect.hypelabs.network-6366f1" /></a>
  <a href="https://dotnet.microsoft.com/"><img src="https://img.shields.io/badge/Made%20With-.NET%2010-512bd4" /></a>
  <a href="https://learn.microsoft.com/openapi/kiota/"><img src="https://img.shields.io/badge/Generated%20With-Kiota-0078d4" /></a>
  <a href="LICENSE"><img src="https://img.shields.io/badge/License-MIT-lightgrey.svg" /></a>
</p>

## Install

Add a reference to the SDK project:

```bash
dotnet add reference ../sdk/src/HypeLabs.Partner.Sdk.csproj
```

Then configure it with your API key (see below).

## Usage

### With dependency injection (recommended)

```csharp
builder.Services.AddPartnerClient(options => options.ApiKey = builder.Configuration["Partner:ApiKey"]!);

// …then inject it anywhere:
public class MyService(PartnerClient client)
{
    public Task<List<Customer>?> GetCustomers() => client.Customers.GetAsync();
}
```

Options can also be bound from configuration:

```csharp
builder.Services.AddPartnerClient(builder.Configuration.GetSection("Partner"));
```

```json
// appsettings.json
{
  "Partner": {
    "ApiKey": "hl_live_…",
    "BaseUrl": null            // optional; defaults to https://connect.hypelabs.network
  }
}
```

### Standalone

```csharp
var client = new PartnerClient("hl_live_…");
var customers = await client.Customers.GetAsync();
```

The API is authenticated with the `X-Api-Key` header, wired up internally — you never touch the Kiota plumbing.
