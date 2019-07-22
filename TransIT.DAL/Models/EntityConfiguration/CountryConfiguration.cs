using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("COUNTRY");

            builder.HasIndex(e => e.Name)
                .HasName("UQ__COUNTRY__D9C1FA008FF4E681")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.CreateDate)
                .HasColumnName("CREATE_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

            builder.Property(e => e.ModDate)
                .HasColumnName("MOD_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("NAME")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.HasOne(d => d.Create)
                .WithMany(p => p.CountryCreate)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK_CREATE_COUNTRY_USER");

            builder.HasOne(d => d.Mod)
                .WithMany(p => p.CountryMod)
                .HasForeignKey(d => d.ModifiedById)
                .HasConstraintName("FK_MOD_COUNTRY_USER");
        }
    }
}
