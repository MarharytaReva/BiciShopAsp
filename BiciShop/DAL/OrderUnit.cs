using BiciShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrderUnit
    {
        [ForeignKey("Bicicleta")]
        public int BicicletaId { get; set; }
        public Bicicleta Bicicleta { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int Count { get; set; }
    }
}
