using System.Collections.Generic;

namespace TransIT.BLL.DTOs
{
    public class StatisticsDTO
    {
        public string FieldName { get; set; }

        public IEnumerable<int> Statistics { get; set; }
    }
}
