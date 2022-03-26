using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiciShop.Models.ViewModels
{
    public class MakeOrderViewModel
    {
        public OrderDTO Order { get; set; }
        public int Id { get; set; }
    }
}
