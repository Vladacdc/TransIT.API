using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models
{
    public class MalfunctionSubgroupConfiguration : IEntityTypeConfiguration<MalfunctionSubgroup>
    {
        public void Configure(EntityTypeBuilder<MalfunctionSubgroup> builder)
        {
            builder.ToTable("MALFUNCTION_SUBGROUP");

            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.CreatedDate)
                .HasColumnName("CREATE_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

            builder.Property(e => e.MalfunctionGroupId).HasColumnName("MALFUNCTION_GROUP_ID");

            builder.Property(e => e.UpdatedDate)
                .HasColumnName("MOD_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.UpdatedById).HasColumnName("MOD_ID");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("NAME");

            builder.HasOne(d => d.Create)
                .WithMany(p => p.MalfunctionSubgroupCreate)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK_CREATE_MALFUNCTION_SUBGROUP_USER");

            builder.HasOne(d => d.MalfunctionGroup)
                .WithMany(p => p.MalfunctionSubgroup)
                .HasForeignKey(d => d.MalfunctionGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MALFUNCTION_SUBGROUP_MALFUNCTION_GROUP");

            builder.HasOne(d => d.Mod)
                .WithMany(p => p.MalfunctionSubgroupMod)
                .HasForeignKey(d => d.UpdatedById)
                .HasConstraintName("FK_MOD_MALFUNCTION_SUBGROUP_USER");
        }
    }
}
