using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models
{
    public class PartsInConfiguration : IEntityTypeConfiguration<PartIn>
    {
        public void Configure(EntityTypeBuilder<PartIn> builder)
        {
            builder.ToTable("PARTS_IN");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.CreatedDate)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.UpdatedDate)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Batch)
                   .IsRequired();

            builder.HasIndex(e => e.Batch)
                   .IsUnique(false)
                   .ForSqlServerIsClustered(false);

            builder.Property(e => e.ArrivalDate)
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.Property(e => e.Price)
                   .IsRequired();

            builder.HasOne(e => e.Currency)
                   .WithMany(e => e.PartInNavigation)
                   .HasForeignKey(e => e.CurrencyId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .IsRequired();

            builder.HasOne(e => e.Unit)
                   .WithMany(e => e.PartsInNavigation)
                   .HasForeignKey(e => e.UnitId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .IsRequired();

            builder.HasOne(e => e.Part)
                   .WithMany(e => e.PartsInNavigation)
                   .HasForeignKey(e => e.PartId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .IsRequired();
        }
    }
}
