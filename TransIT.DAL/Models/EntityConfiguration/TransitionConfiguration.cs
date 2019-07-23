using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models
{
    public class TransitionConfiguration : IEntityTypeConfiguration<Transition>
    {
        public void Configure(EntityTypeBuilder<Transition> builder)
        {
            builder.ToTable("TRANSITION");

            builder.HasIndex(e => new { e.FromStateId, e.ActionTypeId, e.ToStateId })
                .HasName("CK_ISSUE_TRANSITION_UNIQUE")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.ActionTypeId).HasColumnName("ACTION_TYPE_ID");

            builder.Property(e => e.CreatedDate)
                .HasColumnName("CREATE_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

            builder.Property(e => e.FromStateId).HasColumnName("FROM_STATE_ID");

            builder.Property(e => e.IsFixed).HasColumnName("IS_FIXED");

            builder.Property(e => e.UpdatedDate)
                .HasColumnName("MOD_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.UpdatedById).HasColumnName("MOD_ID");

            builder.Property(e => e.ToStateId).HasColumnName("TO_STATE_ID");

            builder.HasOne(d => d.ActionType)
                .WithMany(p => p.Transition)
                .HasForeignKey(d => d.ActionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ACTION_TYPE_ISSUE");

            builder.HasOne(d => d.Create)
                .WithMany(p => p.TransitionCreate)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK_CREATE_ISSUE_TRANSITION_USER");

            builder.HasOne(d => d.FromState)
                .WithMany(p => p.TransitionFromState)
                .HasForeignKey(d => d.FromStateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FROM_STATE");

            builder.HasOne(d => d.Mod)
                .WithMany(p => p.TransitionMod)
                .HasForeignKey(d => d.UpdatedById)
                .HasConstraintName("FK_MOD_ISSUE_TRANSITION_USER");

            builder.HasOne(d => d.ToState)
                .WithMany(p => p.TransitionToState)
                .HasForeignKey(d => d.ToStateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TO_STATE");
        }
    }
}
