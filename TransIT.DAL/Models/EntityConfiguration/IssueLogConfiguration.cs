using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models
{
    public class IssueLogConfiguration : IEntityTypeConfiguration<IssueLog>
    {
        public void Configure(EntityTypeBuilder<IssueLog> builder)
        {
            builder.ToTable("ISSUE_LOG");

            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.ActionTypeId).HasColumnName("ACTION_TYPE_ID");

            builder.Property(e => e.CreatedDate)
                .HasColumnName("CREATE_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

            builder.Property(e => e.Description).HasColumnName("DESCRIPTION");

            builder.Property(e => e.Expenses)
                .HasColumnName("EXPENSES")
                .HasColumnType("decimal(10, 2)");

            builder.Property(e => e.IssueId).HasColumnName("ISSUE_ID");

            builder.Property(e => e.UpdatedDate)
                .HasColumnName("MOD_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.UpdatedById).HasColumnName("MOD_ID");

            builder.Property(e => e.NewStateId).HasColumnName("NEW_STATE_ID");

            builder.Property(e => e.OldStateId).HasColumnName("OLD_STATE_ID");

            builder.Property(e => e.SupplierId).HasColumnName("SUPPLIER_ID");

            builder.Property(e => e.WorkTypeId).HasColumnName("WORK_TYPE_ID");

            builder.HasOne(d => d.ActionType)
                .WithMany(p => p.IssueLog)
                .HasForeignKey(d => d.ActionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ISSUE_LOG_ACTION_TYPE");

            builder.HasOne(d => d.Create)
                .WithMany(p => p.IssueLogCreate)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK_CREATE_ISSUE_LOG_USER");

            builder.HasOne(d => d.Issue)
                .WithMany(p => p.IssueLog)
                .HasForeignKey(d => d.IssueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ISSUE_LOG_ISSUE");

            builder.HasOne(d => d.Mod)
                .WithMany(p => p.IssueLogMod)
                .HasForeignKey(d => d.UpdatedById)
                .HasConstraintName("FK_MOD_ISSUE_LOG_USER");

            builder.HasOne(d => d.NewState)
                .WithMany(p => p.IssueLogNewState)
                .HasForeignKey(d => d.NewStateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NEW_ISSUE_LOG_STATE");

            builder.HasOne(d => d.OldState)
                .WithMany(p => p.IssueLogOldState)
                .HasForeignKey(d => d.OldStateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OLD_ISSUE_LOG_STATE");

            builder.HasOne(d => d.Supplier)
                .WithMany(p => p.IssueLog)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_ISSUE_LOG_SUPPLIER");

            builder.HasOne(i => i.WorkType)
                .WithMany(w => w.IssueLog)
                .HasForeignKey(i => i.WorkTypeId)
                .HasConstraintName("FK_ISSUE_LOG_WORK_TYPE");
        }
    }
}
