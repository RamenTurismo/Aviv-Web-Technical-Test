using System;

namespace listingapi.Infrastructure.Database.Models;

public sealed class ListingPriceHistory
{
    public long Id { get; set; }
    public int ListingId { get; set; }
    public Listing Listing { get; set; }
    public DateTime CreatedAt { get; set; }
    public double Price { get; set; }
}
