using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("DOCUMENT");

            builder.HasIndex(e => e.Path)
                .HasName("UQ_PATH_DOCUMENT")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.CreateDate)
                .HasColumnName("CREATE_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

            builder.Property(e => e.Description).HasColumnName("DESCRIPTION");

            builder.Property(e => e.IssueLogId).HasColumnName("ISSUE_LOG_ID");

            builder.Property(e => e.ModDate)
                .HasColumnName("MOD_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

            builder.Property(e => e.Name)
                .HasColumnName("NAME")
                .HasMaxLength(50);

            builder.Property(e => e.NewDate)
                .HasColumnName("NEW_DATE")
                .HasColumnType("datetime");


            builder.Property(e => e.Path)
                .IsRequired()
                .HasColumnName("PATH")
                .HasMaxLength(500)
                .IsUnicode(true)
                .HasDefaultValueSql("('')");

            builder.HasOne(d => d.Create)
                .WithMany(p => p.DocumentCreate)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK_CREATE_DOCUMENT_USER");

            builder.HasOne(d => d.IssueLog)
                .WithMany(p => p.Document)
                .HasForeignKey(d => d.IssueLogId)
                .HasConstraintName("FK_DOCUMENT_ISSUE_LOG");

            builder.HasOne(d => d.Mod)
                .WithMany(p => p.DocumentMod)
                .HasForeignKey(d => d.ModifiedById)
                .HasConstraintName("FK_MOD_DOCUMENT_USER");
        }
    }
}
