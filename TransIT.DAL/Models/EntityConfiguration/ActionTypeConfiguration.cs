using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models
{
    public class ActionTypeConfiguration : IEntityTypeConfiguration<ActionType>
    {
        public void Configure(EntityTypeBuilder<ActionType> builder)
        {
            builder.ToTable("ACTION_TYPE");

            builder.HasIndex(e => e.Name)
                .HasName("UQ__ACTION_T__D9C1FA00D8EDC403")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.CreateDate)
                .HasColumnName("CREATE_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

            builder.Property(e => e.IsFixed).HasColumnName("IS_FIXED");

            builder.Property(e => e.ModDate)
                .HasColumnName("MOD_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("NAME")
                .HasMaxLength(50);

            builder.HasOne(d => d.Create)
                .WithMany(p => p.ActionTypeCreate)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK_CREATE_ACTION_TYPE_USER");

            builder.HasOne(d => d.Mod)
                .WithMany(p => p.ActionTypeMod)
                .HasForeignKey(d => d.ModifiedById)
                .HasConstraintName("FK_MOD_ACTION_TYPE_USER");
        }
    }
}
