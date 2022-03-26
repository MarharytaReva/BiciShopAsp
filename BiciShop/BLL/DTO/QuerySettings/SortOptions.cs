using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.QuerySettings
{
    public class SortOptions
    {
        public SortOptions()
        {
            FieldSort = FieldSort.None;
            OrderSort = OrderSort.Descreasing;
        }
        public FieldSort FieldSort { get; set; }
        public OrderSort OrderSort { get; set; }
    }
}
