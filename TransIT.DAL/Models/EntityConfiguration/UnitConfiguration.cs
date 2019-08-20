using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models.EntityConfiguration
{
    public class UnitConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.HasIndex(u => u.ShortName).IsUnique();
            builder.Property(u => u.ShortName)
                .HasMaxLength(30)
                .IsRequired();

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
                .WithMany(u => u.UnitCreate)
                .HasForeignKey(u => u.CreatedById);

            builder.HasOne(u => u.Mod)
                .WithMany(u => u.UnitMod)
                .HasForeignKey(u => u.UpdatedById);
        }
    }
}
