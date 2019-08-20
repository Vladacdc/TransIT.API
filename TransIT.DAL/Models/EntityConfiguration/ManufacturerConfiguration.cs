using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models.EntityConfiguration
{
    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.HasIndex(u => u.Name).IsUnique();
            builder.Property(u => u.Name)
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(u => u.Create)
                .WithMany(u => u.ManufacturerCreate)
                .HasForeignKey(u => u.CreatedById);

            builder.HasOne(u => u.Mod)
                .WithMany(u => u.ManufacturerMod)
                .HasForeignKey(u => u.UpdatedById);
        }
    }
}