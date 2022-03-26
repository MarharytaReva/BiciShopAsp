using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.QuerySettings
{
    public class OrderQueryOptions
    {
        public OrderSort OrderSort { get; set; }
        public int PhaseId { get; set; }
        public int PageNumber { get; set; }
        public OrderQueryOptions()
        {
            OrderSort = OrderSort.Descreasing;
            PageNumber = 1;
        }
    }
}
