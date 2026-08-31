# HypeLabs Partner SDK

A C# client for the [HypeLabs Partner API](https://connect.hypelabs.network), for internal .NET services.

## Install

Reference the project (or the built package) and configure it with your API key.

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

## Keeping the client up to date

The `Generated/` folder is a Kiota client generated from the live OpenAPI description and **committed** to the
repo, so builds are offline and reproducible. When the API changes and you want to adopt it, regenerate:

```bash
./regen.sh                 # pulls from https://connect.hypelabs.network/openapi/v1.json
git diff Generated/        # review what changed
git commit -am "Regenerate SDK client"
```

`regen.sh` requires the Kiota CLI: `dotnet tool install --global Microsoft.OpenApi.Kiota`.
Point it elsewhere (e.g. staging) with `OPENAPI_URL=… ./regen.sh`.
