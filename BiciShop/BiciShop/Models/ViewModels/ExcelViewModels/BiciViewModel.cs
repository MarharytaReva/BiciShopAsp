using BLL.DTO.QuerySettings;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiciShop.Models.ViewModels.ExcelViewModels
{
    public class BiciViewModel
    {
        public QueryOptions QueryOptions { get; set; }
        public int PageCount { get; set; }
        public int Offset { get; set; }
        public SelectList Colors { get; set; }
        public SelectList BiciTypes { get; set; }

        public SelectList SortFields { get; set; }
        public SelectList SortOrders { get; set; }
        public string ErrorText { get; set; }
        public BiciViewModel()
        {
            QueryOptions = new QueryOptions();
        }
    }
}
