using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models
{
    public class MalfunctionGroupConfiguration : IEntityTypeConfiguration<MalfunctionGroup>
    {
        public void Configure(EntityTypeBuilder<MalfunctionGroup> builder)
        {
            builder.ToTable("MALFUNCTION_GROUP");

            builder.Property(e => e.Id).HasColumnName("ID");

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

            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("NAME");

            builder.HasOne(d => d.Create)
                .WithMany(p => p.MalfunctionGroupCreate)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK__MALFUNCTI__CREAT__73BA3083");

            builder.HasOne(d => d.Mod)
                .WithMany(p => p.MalfunctionGroupMod)
                .HasForeignKey(d => d.UpdatedById)
                .HasConstraintName("FK__MALFUNCTI__MOD_I__74AE54BC");
        }
    }
}
