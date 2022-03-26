using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.QuerySettings
{
    public class QueryOptions
    {
        public SortOptions SortOptions { get; set; }
        public FiltrationOptions FiltrationOptions { get; set; }
        public string SearchText { get; set; }
        public int PageNumber { get; set; }
        public QueryOptions()
        {
            SortOptions = new SortOptions();
            FiltrationOptions = new FiltrationOptions();
            PageNumber = 1;
        }
    }
}
