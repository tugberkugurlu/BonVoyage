# BonVoyage [![Build Status](https://travis-ci.org/tugberkugurlu/BonVoyage.svg?branch=master)](https://travis-ci.org/tugberkugurlu/BonVoyage)

Foursquare .NET Client that you will fall in love with :heart: This library supports [.NET Standard 1.1](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard1.1.md).

## Getting the Library

BonVoyage Foursquare .NET Client is [available on NuGet](https://www.nuget.org/packages/BonVoyage) and can be integrated into your project through the usual ways.

## API Usage

One of the great things about BonVoyage is its semantic API. Each resource you get a handle of (e.g. Category, Venue) will expose its aspects (e.g. Photos) and actions (e.g. Like, Dislike). You can have a glimpse of the API of the library below.

### Userless Access

The simple usage of the API for userless access:

```csharp
var userlessSettings = new UserlessAccessSettings("CLIENT-ID", "CLIENT-SECRET");

using (var bonVoyageContext = new BonVoyageContext())
using (var foursquareContext = bonVoyageContext.CreateUserlessFoursquareContext(userlessSettings))
{
    // Get and itirate over top level categories
    var categories = await foursquareContext.Categories.Get();
    foreach (var venueCategory in categories)
    {
        Console.WriteLine("{0}: {1}", venueCategory.Id, venueCategory.Name);

        // Let's now get venues in San Fransisco for each category
        var venues = await venueCategory.SearchVenues("San Fransisco, CA", 10);
        foreach (var venue in venues)
        {
            Console.WriteLine("\t{0}: {1}", venue.Id, venue.Name);

            // Finally, get the photos of each venue
            var photos = await venue.GetPhotos();
            foreach (var photo in photos)
            {
                Console.WriteLine("{0}: {1}", photo.GetUrl(), photo.Visibility);
            }
        }
    }
}
```

You can see the [Playground](./samples/Playground) example for some more details on the usage.
