using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        public string UserKey { get; set; }

        public DateTime Date { get; set; }

        public HandlePhaseDTO HandlePhase { get; set; }

        public List<OrderUnitDTO> OrderUnits { get; set; }
        public int TotalValue { get; set; }
        public int GetTotalValue()
        {
            return OrderUnits.Sum(x => x.GetValue());
        }
    }
}
