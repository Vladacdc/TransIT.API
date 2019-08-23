namespace TransIT.DAL.Models.Entities
{
    public class SupplierPart
    {
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
        public int PartId { get; set; }
        public virtual Part Part { get; set; }
    }
}