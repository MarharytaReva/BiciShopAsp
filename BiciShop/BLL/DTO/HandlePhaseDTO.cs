using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class HandlePhaseDTO
    {
        public int HandlePhaseId { get; set; }

        [Required(ErrorMessage = "Name of phase is required")]
        public string PhaseName { get; set; }
        public bool IsCancelAvaliable { get; set; }
    }
}
