using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models
{
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("BILL");

            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.CreatedDate)
                .HasColumnName("CREATE_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

            builder.Property(e => e.DocumentId).HasColumnName("DOCUMENT_ID");

            builder.Property(e => e.IssueId).HasColumnName("ISSUE_ID");

            builder.Property(e => e.UpdatedDate)
                .HasColumnName("MOD_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.UpdatedById).HasColumnName("MOD_ID");

            builder.Property(e => e.Sum)
                .HasColumnName("SUM")
                .HasColumnType("decimal(20, 2)");

            builder.HasOne(d => d.Create)
                .WithMany(p => p.BillCreate)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK_CREATE_BILL_USER");

            builder.HasOne(d => d.Document)
                .WithMany(p => p.Bill)
                .HasForeignKey(d => d.DocumentId)
                .HasConstraintName("FK_BILL_DOCUMENT");

            builder.HasOne(d => d.Issue)
                .WithMany(p => p.Bill)
                .HasForeignKey(d => d.IssueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BILL_ISSUE");

            builder.HasOne(d => d.Mod)
                .WithMany(p => p.BillMod)
                .HasForeignKey(d => d.UpdatedById)
                .HasConstraintName("FK_MOD_BILL_USER");

        }
    }
}
