using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EfCore.Configurations;

public class ReservationConfig : BaseConfig<Reservation>
{
    public override void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable($"{nameof(Reservation).ToLower()}s");

        base.Configure(builder);

        builder
            .Property(x => x.RentDate)
            .HasConversion
            (
                src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
            )
            .IsRequired();

        builder
            .Property(x => x.ExpectingRetrieveDate)
            .HasConversion
            (
                src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
            )
            .IsRequired();

        builder
            .Property(x => x.CarId)
            .IsRequired();

        builder
            .Property(x => x.CustomerId)
            .IsRequired();

        builder
            .HasOne<Customer>(x => x.Customer)
            .WithMany(x => x.Reservations)
            .HasForeignKey(x => x.CustomerId);

        builder
            .HasOne<Car>(x => x.Car)
            .WithMany(x => x.Reservations)
            .HasForeignKey(x => x.CarId);

        builder
            .Property(x => x.RentDate)
            .HasConversion
            (
                src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
            )
            .IsRequired();
    }
}
