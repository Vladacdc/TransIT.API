using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models.EntityConfiguration
{
    class WorkTypeConfiguration: IEntityTypeConfiguration<WorkType>
    {
        public void Configure(EntityTypeBuilder<WorkType> builder)
        {
            builder.ToTable("WORK_TYPE");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.Name).IsRequired().HasColumnName("NAME").HasMaxLength(50);

            builder.Property(e => e.EstimatedTime).IsRequired().HasColumnName("ESTIMATED_TIME");

            builder.Property(e => e.EstimatedCost).IsRequired().HasColumnName("ESTIMATED_COST");

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

            builder.HasOne(d => d.Create)
                .WithMany(p => p.WorkTypeCreate)
                .HasForeignKey(d => d.CreatedById);

            builder.HasOne(d => d.Mod)
                .WithMany(p => p.WorkTypeMod)
                .HasForeignKey(d => d.UpdatedById);
        }
    }
}
