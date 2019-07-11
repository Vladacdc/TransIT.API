namespace TransIT.BLL.DTOs
{
    public class MalfunctionDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public MalfunctionSubgroupDTO MalfunctionSubgroup { get; set; }
    }
}
