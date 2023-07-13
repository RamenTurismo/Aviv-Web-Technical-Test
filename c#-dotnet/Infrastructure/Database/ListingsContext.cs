using listingapi.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace listingapi.Infrastructure.Database;

public class ListingsContext : DbContext
{
    public ListingsContext(DbContextOptions<ListingsContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Listing>(entity =>
        {
            entity.ToTable("listing");
            entity.HasKey(e => e.Id);

            // need https://github.com/efcore/EFCore.NamingConventions
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.BuildingType).HasColumnName("building_type");
            entity.Property(e => e.StreetAddress).HasColumnName("street_address");
            entity.Property(e => e.PostalCode).HasColumnName("postal_code");
            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.Country).HasColumnName("country");
            entity.Property(e => e.SurfaceAreaM2).HasColumnName("surface_area_m2");
            entity.Property(e => e.RoomsCount).HasColumnName("rooms_count");
            entity.Property(e => e.BedroomsCount).HasColumnName("bedrooms_count");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ContactPhoneNumber).HasColumnName("contact_phone_number");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
        });

        modelBuilder.Entity<ListingPriceHistory>(entity =>
        {
            entity.ToTable("listing_price_history");

            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Listing)
                .WithMany(e => e.ListingPriceChanges)
                .HasForeignKey(e => e.ListingId);

            entity.Property(e => e.ListingId).HasColumnName("listing_id");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
        });
    }

    public virtual DbSet<Listing> Listings { get; set; }
}
