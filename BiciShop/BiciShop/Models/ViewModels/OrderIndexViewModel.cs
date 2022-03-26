using BLL.DTO;
using BLL.DTO.QuerySettings;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiciShop.Models.ViewModels
{
    public class OrderIndexViewModel
    {
        public IEnumerable<OrderDTO> Orders { get; set; }
        public SelectList SortOrders { get; set; }
        public OrderQueryOptions OrderQueryOptions { get; set; }
        public int PageCount { get; set; }
        public bool FirstLoad { get; set; }
        public OrderIndexViewModel()
        {
            OrderQueryOptions = new OrderQueryOptions();
        }
    }
}
