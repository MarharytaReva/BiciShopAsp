using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class BiciTypeDTO
    {
        public int BiciTypeId { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum 50 characters allowed")]
        public string BiciTypeName { get; set; }
    }
}
