using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BiciShop.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        
        public string Name { get; set; }
        
        public string Lastname { get; set; }
        
        public string Phone { get; set; }
        
        public string Email { get; set; }

        public string UserKey { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("HandlePhase")]
        public int HandlePhaseId { get; set; }
        public HandlePhase HandlePhase { get; set; }
       
        public int TotalValue { get; set; }
        public List<OrderUnit> OrderUnits { get; set; }
    }
}
