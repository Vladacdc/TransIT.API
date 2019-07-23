using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasIndex(e => e.TransName)
                .HasName("UQ_ROLE_TRANS_NAME")
                .IsUnique();

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

            builder.Property(e => e.TransName)
                .HasColumnName("TRANS_NAME")
                .HasMaxLength(50);

            builder.HasOne(d => d.Create)
                .WithMany(p => p.RoleCreatedByNavigation)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK_CREATE_ROLE_USER");

            builder.HasOne(d => d.Mod)
                .WithMany(p => p.RoleModifiedByNavigation)
                .HasForeignKey(d => d.UpdatedById)
                .HasConstraintName("FK_MOD_ROLE_USER");
        }
    }
}
