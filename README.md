# BonVoyage [![Build Status](https://travis-ci.org/tugberkugurlu/BonVoyage.svg?branch=master)](https://travis-ci.org/tugberkugurlu/BonVoyage)

Foursquare .NET Client that you will fall in love with :heart: This library supports [.NET Standard 1.1](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard1.1.md).

## Getting the Library

BonVoyage Foursquare .NET Client is [available on NuGet](https://www.nuget.org/packages/BonVoyage) and can be integrated into your project through the usual ways.

## API Usage

### Userless Access

Here is the simple usage of the API for userless access:

```csharp
var userlessSettings = new UserlessAccessSettings("CLIENT-ID", "CLIENT-SECRET");

using (var bonVoyageContext = new BonVoyageContext())
using (var foursquareContext = bonVoyageContext.CreateUserlessFoursquareContext(userlessSettings))
{
    var categories = await foursquareContext.Categories.Get();
    foreach (var venueCategory in categories)
    {
        Console.WriteLine("{0}: {1}", venueCategory.Id, venueCategory.Name);
    }
}
```

You can see the [Playground](./samples/Playground) example for some more details on the usage.
