using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EfCore.Configurations;

public class CustomerConfig : BaseConfig<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable($"{nameof(Customer).ToLower()}s");

        base.Configure(builder);

        builder
            .Property(x => x.FirstName)
            .HasColumnType("Varchar")
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(x => x.LastName)
            .HasColumnType("Varchar")
            .HasMaxLength(100)
            .IsRequired();
        
        builder
            .Property(x => x.City)
            .HasColumnType("Varchar")
            .HasMaxLength(150)
            .IsRequired();

        builder
            .Property(x => x.Street)
            .HasColumnType("Varchar")
            .HasMaxLength(250)
            .IsRequired();
    }
}
