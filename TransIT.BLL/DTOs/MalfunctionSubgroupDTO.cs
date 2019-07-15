namespace TransIT.BLL.DTOs
{
    public class MalfunctionSubgroupDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public MalfunctionGroupDTO MalfunctionGroup { get; set; }
    }
}
