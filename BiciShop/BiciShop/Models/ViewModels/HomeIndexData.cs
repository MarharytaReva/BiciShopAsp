using BLL.DTO;
using BLL.DTO.QuerySettings;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiciShop.Models
{
    public class HomeIndexData
    {
        public QueryOptions QueryOptions { get; set; }
        public IEnumerable<BicicletaDTO> Bicicletas { get; set; }
       
        public int PageCount { get; set; }
        public SelectList Colors { get; set; }
        public SelectList BiciTypes { get; set; }

        public SelectList SortFields { get; set; }
        public SelectList SortOrders { get; set; }
        public string Text { get; set; }
        public bool FirstLoad { get; set; }
        public HomeIndexData()
        {
            QueryOptions = new QueryOptions();
        }
    }
}
