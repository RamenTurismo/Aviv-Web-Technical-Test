using System;
using System.Collections;
using System.Collections.Generic;

namespace listingapi.Infrastructure.Database.Models;

public sealed class Listing
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string BuildingType { get; set; }
    public string StreetAddress { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public double SurfaceAreaM2 { get; set; }
    public int RoomsCount { get; set; }
    public int BedroomsCount { get; set; }
    public double Price { get; set; }
    public string ContactPhoneNumber { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public ICollection<ListingPriceHistory> ListingPriceChanges { get; set; }

    public Listing()
    {
        ListingPriceChanges = new HashSet<ListingPriceHistory>();
    }
}
