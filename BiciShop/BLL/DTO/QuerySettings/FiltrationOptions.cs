using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.QuerySettings
{
    public class FiltrationOptions
    {
        public string Color { get; set; }
        public string BiciType { get; set; }
        public FiltrationOptions()
        {
            Color = BiciService.DefaultStr;
            BiciType = BiciService.DefaultStr;
        }
    }
}
