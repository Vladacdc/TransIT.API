using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.CreatedDate)
                .HasColumnName("CREATE_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

            builder.Property(e => e.FirstName)
                .HasColumnName("FIRST_NAME")
                .HasMaxLength(50);

            builder.Property(e => e.IsActive)
                .HasColumnName("IS_ACTIVE")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.LastName)
                .HasColumnName("LAST_NAME")
                .HasMaxLength(50);

            builder.Property(e => e.MiddleName)
                .HasColumnName("MIDDLE_NAME")
                .HasMaxLength(50);

            builder.Property(e => e.UpdatedDate)
                .HasColumnName("MOD_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.UpdatedById).HasColumnName("MOD_ID");

            builder.HasOne(d => d.CreatedBy)
                .WithMany(p => p.InverseCreate)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK_CREATE_USER");

            builder.HasOne(d => d.ModifiedBy)
                .WithMany(p => p.InverseMod)
                .HasForeignKey(d => d.UpdatedById)
                .HasConstraintName("FK_MOD_USER");
        }
    }
}
