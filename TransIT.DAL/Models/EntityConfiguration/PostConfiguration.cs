using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("POST");

            builder.HasIndex(e => e.Name)
                .HasName("UQ__POST__D9C1FA00297EABB2")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.CreateDate)
                .HasColumnName("CREATE_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

            builder.Property(e => e.ModDate)
                .HasColumnName("MOD_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

            builder.Property(e => e.Name)
                .HasColumnName("NAME")
                .HasMaxLength(50);

            builder.HasOne(d => d.Create)
                .WithMany(p => p.PostCreate)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK_MOD_POST_USER");

            builder.HasOne(d => d.Mod)
                .WithMany(p => p.PostMod)
                .HasForeignKey(d => d.ModifiedById)
                .HasConstraintName("FK_MOD_POST_ROLE");
        }
    }
}
