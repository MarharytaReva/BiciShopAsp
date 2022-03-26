using BLL.DTO;
using BLL.DTO.QuerySettings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiciShop.Models.ViewModels.ExcelViewModels
{
    public class OrderExcelViewModel
    {
        public int Offset { get; set; }
        public SelectList SortOrders { get; set; }
        public SelectList Phases { get; set; }
        public SelectList PhasesForEdit { get; set; }
        public OrderQueryOptions OrderQueryOptions { get; set; }
        public IFormFile postedFile { get; set; }
        public string ErrorText { get; set; }
        public OrderExcelViewModel()
        {
            OrderQueryOptions = new OrderQueryOptions();
        }
    }
}
