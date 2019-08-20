using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models
{
    public class PartConfiguration : IEntityTypeConfiguration<Part>
    {
        public void Configure(EntityTypeBuilder<Part> builder)
        {
            builder.ToTable("PARTS");

            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.Name).HasColumnName("NAME");

            builder.Property(e => e.Code).HasColumnName("CODE");

            builder.Property(e => e.UnitId).HasColumnName("UNIT");

            builder.Property(e => e.ManufacturerId).HasColumnName("MANUFACTURER");
                
            builder.Property(e => e.CreatedDate)
                .HasColumnName("CREATE_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

            builder.Property(e => e.UpdatedDate)
                .HasColumnName("MOD_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.UpdatedById).HasColumnName("MOD_ID");

            builder.HasOne(e => e.Manufacturer)
                .WithMany(m => m.Parts)
                .HasForeignKey(p => p.ManufacturerId)
                .HasConstraintName("FK_PART_MANUFACTURER");

            builder.HasOne(e => e.Unit)
               .WithMany(m => m.Parts)
               .HasForeignKey(p => p.UnitId)
               .HasConstraintName("FK_PART_UNIT");
        }
    }
}
