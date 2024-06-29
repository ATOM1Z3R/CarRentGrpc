using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EfCore.Configurations;

public class EmployeeConfig : BaseConfig<Employee>
{
    public override void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable($"{nameof(Employee).ToLower()}s");

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
            .Property(x => x.Password)
            .IsRequired();

    }
}
