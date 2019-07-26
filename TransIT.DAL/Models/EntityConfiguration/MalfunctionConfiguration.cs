using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models
{
    public class MalfunctionConfiguration : IEntityTypeConfiguration<Malfunction>
    {
        public void Configure(EntityTypeBuilder<Malfunction> builder)
        {
            builder.ToTable("MALFUNCTION");

            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.CreatedDate)
                .HasColumnName("CREATE_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

            builder.Property(e => e.MalfunctionSubgroupId).HasColumnName("MALFUNCTION_SUBGROUP_ID");

            builder.Property(e => e.UpdatedDate)
                .HasColumnName("MOD_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.UpdatedById).HasColumnName("MOD_ID");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("NAME");

            builder.HasOne(d => d.Create)
                .WithMany(p => p.MalfunctionCreate)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK_CREATE_MALFUNCTION_ROLE");

            builder.HasOne(d => d.MalfunctionSubgroup)
                .WithMany(p => p.Malfunction)
                .HasForeignKey(d => d.MalfunctionSubgroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MALFUNCTION_SUBGROUP_MALFUNCTION_SUBGROUP");

            builder.HasOne(d => d.Mod)
                .WithMany(p => p.MalfunctionMod)
                .HasForeignKey(d => d.UpdatedById)
                .HasConstraintName("FK_MOD_MALFUNCTION_USER");
        }
    }
}
