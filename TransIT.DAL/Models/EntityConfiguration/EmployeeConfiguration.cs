using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("EMPLOYEE");

            builder.HasIndex(e => e.BoardNumber)
                .HasName("UQ_EMPLOYEE_BOARD_NUMBER_UNIQUE")
                .IsUnique();

            builder.HasIndex(e => e.ShortName)
                .HasName("UQ_EMPLOYEE_SHORT_NAME_UNIQUE")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.PostId).HasColumnName("POST_ID");

            builder.Property(e => e.BoardNumber).HasColumnName("BOARD_NUMBER");

            builder.Property(e => e.CreatedDate)
                .HasColumnName("CREATE_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

            builder.Property(e => e.FirstName)
                .HasColumnName("FIRST_NAME")
                .HasMaxLength(50);

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

            builder.Property(e => e.PostId).HasColumnName("POST_ID");

            builder.Property(e => e.ShortName)
                .IsRequired()
                .HasColumnName("SHORT_NAME")
                .HasMaxLength(50);

            builder.HasOne(d => d.Create)
                .WithMany(p => p.EmployeeCreate)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK_MOD_EMPLOYEE_USER");

            builder.HasOne(d => d.Mod)
                .WithMany(p => p.EmployeeMod)
                .HasForeignKey(d => d.UpdatedById)
                .HasConstraintName("FK_MOD_EMPLOYEE_ROLE");

            builder.HasOne(d => d.Post)
                .WithMany(p => p.Employee)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EMPLOYEE_POST");

            builder.HasIndex(e => e.AttachedUserId)
                .IsUnique();

            builder.HasOne(e => e.AttachedUser)
                .WithMany(u => u.Employees)
                .HasForeignKey(e => e.AttachedUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_ATTACHED_USER");
        }
    }
}
