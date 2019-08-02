using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("SUPPLIER");

            builder.HasIndex(e => e.Name)
                .HasName("UQ__SUPPLIER__D9C1FA0021944BFA")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.CountryId).HasColumnName("COUNTRY");

            builder.Property(e => e.CreatedDate)
                .HasColumnName("CREATE_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

            builder.Property(e => e.CurrencyId).HasColumnName("CURRENCY");

            builder.Property(e => e.Edrpou)
                .IsRequired()
                .HasColumnName("EDRPOU")
                .HasMaxLength(14);

            builder.Property(e => e.FullName)
                .IsRequired()
                .HasColumnName("FULL_NAME")
                .HasMaxLength(500);

            builder.Property(e => e.UpdatedDate)
                .HasColumnName("MOD_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.UpdatedById).HasColumnName("MOD_ID");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("NAME")
                .HasMaxLength(50);

            builder.HasOne(d => d.Country)
                .WithMany(p => p.Supplier)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_Country");

            builder.HasOne(d => d.Create)
                .WithMany(p => p.SupplierCreate)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK_CREATE_SUPPLIER_USER");

            builder.HasOne(d => d.Currency)
                .WithMany(p => p.Supplier)
                .HasForeignKey(d => d.CurrencyId)
                .HasConstraintName("FK_Currency");

            builder.HasOne(d => d.Mod)
                .WithMany(p => p.SupplierMod)
                .HasForeignKey(d => d.UpdatedById)
                .HasConstraintName("FK_MOD_SUPPLIER_USER");
        }
    }
}
