using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EfCore.Configurations;

public abstract class BaseConfig<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Base
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .Property(p => p.Created)
            .HasConversion
            (
                src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
            );

        builder
            .Property(p => p.Deleted)
            .HasConversion
            (
                src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
            );
    }
}
