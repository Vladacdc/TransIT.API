using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models
{
    public class IssueLogPartsConfiguration : IEntityTypeConfiguration<IssueLogParts>
    {
        public void Configure(EntityTypeBuilder<IssueLogParts> builder)
        {
            builder.HasKey(ur => new { ur.IssueLogId, ur.PartId });

            builder.HasOne(ur => ur.IssueLog)
                .WithMany(r => r.IssueLogParts)
                .HasForeignKey(ur => ur.IssueLogId)
                .IsRequired();

            builder.HasOne(ur => ur.Part)
                .WithMany(r => r.IssueLogParts)
                .HasForeignKey(ur => ur.PartId)
                .IsRequired();
        }
    }
}
