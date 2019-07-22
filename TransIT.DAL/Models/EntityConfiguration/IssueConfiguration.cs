using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models
{
    public class IssueConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            builder.ToTable("ISSUE");

            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.AssignedToId).HasColumnName("ASSIGNED_TO");

            builder.Property(e => e.CreateDate)
                .HasColumnName("CREATE_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

            builder.Property(e => e.Date)
                .HasColumnName("DATE")
                .HasColumnType("date")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Deadline)
                .HasColumnName("DEADLINE")
                .HasColumnType("datetime");

            builder.Property(e => e.MalfunctionId).HasColumnName("MALFUNCTION_ID");

            builder.Property(e => e.ModDate)
                .HasColumnName("MOD_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

            builder.Property(e => e.Number).HasColumnName("NUMBER");

            builder.Property(e => e.Priority).HasColumnName("PRIORITY");

            builder.Property(e => e.StateId)
                .HasColumnName("STATE_ID")
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.Summary).HasColumnName("SUMMARY");

            builder.Property(e => e.VehicleId).HasColumnName("VEHICLE_ID");

            builder.Property(e => e.Warranty).HasColumnName("WARRANTY");

            builder.HasOne(d => d.AssignedTo)
                .WithMany(p => p.Issue)
                .HasForeignKey(d => d.AssignedToId);

            builder.HasOne(d => d.Create)
                .WithMany(p => p.IssueCreate)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK_CREATE_ISSUE_USER");

            builder.HasOne(d => d.Malfunction)
                .WithMany(p => p.Issue)
                .HasForeignKey(d => d.MalfunctionId)
                .HasConstraintName("FK_ISSUE_MALFUNCTION");

            builder.HasOne(d => d.Mod)
                .WithMany(p => p.IssueMod)
                .HasForeignKey(d => d.ModifiedById)
                .HasConstraintName("FK_MOD_ISSUE_USER");

            builder.HasOne(d => d.State)
                .WithMany(p => p.Issue)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ISSUE_STATE");

            builder.HasOne(d => d.Vehicle)
                .WithMany(p => p.Issue)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ISSUE_VEHICLE");
        }
    }
}
