namespace TransIT.BLL.DTOs
{
    public class TransitionDTO
    {
        public int Id { get; set; }
        public virtual ActionTypeDTO ActionType { get; set; }
        public virtual StateDTO FromState { get; set; }
        public virtual StateDTO ToState { get; set; }
        public bool IsFixed { get; set; }
    }
}
