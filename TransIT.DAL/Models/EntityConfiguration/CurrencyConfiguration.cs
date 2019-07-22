using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("CURRENCY");

            builder.HasIndex(e => e.ShortName)
                .HasName("UQ__CURRENCY__F4E7E33EEBE730B7")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.CreateDate)
                .HasColumnName("CREATE_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

            builder.Property(e => e.FullName)
                .IsRequired()
                .HasColumnName("FULL_NAME")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.ModDate)
                .HasColumnName("MOD_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

            builder.Property(e => e.ShortName)
                .IsRequired()
                .HasColumnName("SHORT_NAME")
                .HasMaxLength(5)
                .IsUnicode(false);

            builder.HasOne(d => d.Create)
                .WithMany(p => p.CurrencyCreate)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK_CREATE_CURRENCY_USER");

            builder.HasOne(d => d.Mod)
                .WithMany(p => p.CurrencyMod)
                .HasForeignKey(d => d.ModifiedById)
                .HasConstraintName("FK_MOD_CURRENCY_USER");
        }
    }
}
