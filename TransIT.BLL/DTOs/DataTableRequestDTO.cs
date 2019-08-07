using System;
using System.Collections.Generic;
using System.Linq;

namespace TransIT.BLL.DTOs
{
    public class DataTableRequestDTO
    {
        public const string DataTableAscending = "asc";
        public const string DataTableDescending = "desc";
        
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public SearchType Search { get; set; }
        public ColumnType[] Columns { get; set; }
        public OrderType[] Order { get; set; }
        public List<FilterType> Filters { get; set; }

        public IEnumerable<string> SearchEntries
        {
            get
            {
                if (Search != null && Search.Value != null)
                {
                    return Search.Value.Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => x.Trim().ToUpperInvariant());
                }
                else
                {
                    return Enumerable.Empty<string>();
                }
            }
        }

        public class OrderType
        {
            public int Column { get; set; }
            public string Dir { get; set; }
        }

        public class SearchType
        {
            public string Value { get; set; }
            public bool Regex { get; set; }
        }

        public class ColumnType
        {
            public string Name { get; set; }
            public string Title { get; set; }
            public string Data { get; set; }
            public bool Searchable { get; set; }
            public bool Orderable { get; set; }
            public SearchType Search { get; set; }
        }

        public class FilterType
        {
            public string EntityPropertyPath { get; set; }
            public string Value { get; set; }
            public string Operator { get; set; } = "==";
        }
    }
}
