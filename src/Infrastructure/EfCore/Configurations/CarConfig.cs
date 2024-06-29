using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EfCore.Configurations;

public class CarConfig : BaseConfig<Car>
{
    public override void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable($"{nameof(Car).ToLower()}s");

        base.Configure(builder);

        builder
            .Property(x => x.NumberPlate)
            .HasColumnType("Varchar")
            .HasMaxLength(15);

        builder
            .HasIndex(x => x.NumberPlate)
            .IsUnique();

        builder
            .Property(x => x.Manufacturer)
            .HasColumnType("Varchar")
            .HasMaxLength(125)
            .IsRequired();

        builder
            .Property(x => x.Model)
            .HasColumnType("Varchar")
            .HasMaxLength(125)
            .IsRequired();

        builder
            .Property(x => x.Color)
            .HasColumnType("Varchar")
            .HasMaxLength(75)
            .IsRequired();

        builder
            .Property(x => x.NumberOfSeats)
            .IsRequired();

        builder
            .Property(x => x.Year)
            .IsRequired();

        builder
            .Property(x => x.Availability)
            .IsRequired();

        builder
            .Property(x => x.PriceMultiplier)
            .IsRequired();

        builder
            .HasOne<Localization>(x => x.Localization)
            .WithMany(x => x.Cars)
            .HasForeignKey(x => x.LocalizationId);
    }
}
