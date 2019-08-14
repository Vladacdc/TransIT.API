using System.Collections.Generic;

namespace TransIT.BLL.DTOs
{
    public class StatisticsDTO
    {
        public string FieldName { get; set; }

        public ICollection<int> Statistics { get; set; }
    }
}
