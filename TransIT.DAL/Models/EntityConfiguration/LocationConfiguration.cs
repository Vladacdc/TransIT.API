using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("LOCATION");

            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.CreateDate)
                .HasColumnName("CREATE_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

            builder.Property(e => e.Description).HasColumnName("DESCRIPTION");

            builder.Property(e => e.ModDate)
                .HasColumnName("MOD_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

            builder.Property(e => e.Name)
                .HasColumnName("NAME")
                .HasMaxLength(50);

            builder.HasOne(d => d.Create)
                .WithMany(p => p.LocationCreate)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK_CREATE_LOCATION_USER");

            builder.HasOne(d => d.Mod)
                .WithMany(p => p.LocationMod)
                .HasForeignKey(d => d.ModifiedById)
                .HasConstraintName("FK_MOD_LOCATION_USER");
        }
    }
}
