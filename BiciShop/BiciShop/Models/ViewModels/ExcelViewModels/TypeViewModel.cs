using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiciShop.Models.ViewModels.ExcelViewModels
{
    public class TypeViewModel
    {
        public IEnumerable<BiciTypeDTO> BiciTypes { get; set; }
        public string ErrorText { get; set; }
    }
}
