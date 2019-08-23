using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models
{
    public class SuppliersPartsConfiguration : IEntityTypeConfiguration<SuppliersParts>
    {
        public void Configure(EntityTypeBuilder<SuppliersParts> builder)
        {
            builder.HasKey(ur => new { ur.SupplierId, ur.PartId });

            builder.HasOne(ur => ur.Supplier)
                .WithMany(r => r.SuppliersParts)
                .HasForeignKey(ur => ur.SupplierId)
                .IsRequired();

            builder.HasOne(ur => ur.Part)
                .WithMany(r => r.SuppliersParts)
                .HasForeignKey(ur => ur.PartId)
                .IsRequired();
        }
    }
}
