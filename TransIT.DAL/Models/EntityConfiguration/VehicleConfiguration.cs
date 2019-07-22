using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("VEHICLE");

            builder.HasIndex(e => e.Vincode)
                .HasName("UQ_VINCODE_UNIQUE")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.Brand)
                .HasColumnName("BRAND")
                .HasMaxLength(50);

            builder.Property(e => e.CommissioningDate)
                .HasColumnName("COMMISSIONING_DATE")
                .HasColumnType("datetime");

            builder.Property(e => e.CreateDate)
                .HasColumnName("CREATE_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.CreatedById).HasColumnName("CREATE_ID");

            builder.Property(e => e.InventoryId)
                .HasColumnName("INVENTORY_ID")
                .HasMaxLength(40);

            builder.Property(e => e.LocationId).HasColumnName("LOCATION_ID");

            builder.Property(e => e.ModDate)
                .HasColumnName("MOD_DATE")
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ModifiedById).HasColumnName("MOD_ID");

            builder.Property(e => e.Model)
                .HasColumnName("MODEL")
                .HasMaxLength(50);

            builder.Property(e => e.RegNum)
                .HasColumnName("REG_NUM")
                .HasMaxLength(15);

            builder.Property(e => e.VehicleTypeId).HasColumnName("VEHICLE_TYPE_ID");

            builder.Property(e => e.Vincode)
                .IsRequired()
                .HasColumnName("VINCODE")
                .HasMaxLength(20);

            builder.Property(e => e.WarrantyEndDate)
                .HasColumnName("WARRANTY_END_DATE")
                .HasColumnType("datetime");

            builder.HasOne(d => d.Create)
                .WithMany(p => p.VehicleCreate)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK_MOD_VEHICLE_USER");

            builder.HasOne(d => d.Location)
                .WithMany(p => p.Vehicle)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK_VEHICLE_LOCATION");

            builder.HasOne(d => d.Mod)
                .WithMany(p => p.VehicleMod)
                .HasForeignKey(d => d.ModifiedById)
                .HasConstraintName("FK_MOD_VEHICLE_ROLE");

            builder.HasOne(d => d.VehicleType)
                .WithMany(p => p.Vehicle)
                .HasForeignKey(d => d.VehicleTypeId)
                .HasConstraintName("FK_VEHICLE_VEHICLE_TYPE");
        }
    }
}
