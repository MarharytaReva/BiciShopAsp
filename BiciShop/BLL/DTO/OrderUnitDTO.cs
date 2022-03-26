using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class OrderUnitDTO
    {
        public int BicicletaId { get; set; }
        public BicicletaDTO Bicicleta { get; set; }


        public int OrderId { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "The count must not be less than 1")]
        public int Count { get; set; }

        public int GetValue()
        {
            return (Count * Bicicleta.GetValueWithDiscount());
        }
    }
}
