using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EfCore.Configurations;

public class LocalizationConfig : BaseConfig<Localization>
{
    public override void Configure(EntityTypeBuilder<Localization> builder)
    {
        builder.ToTable($"{nameof(Localization).ToLower()}s");

        base.Configure(builder);

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
