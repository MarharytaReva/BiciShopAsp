using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class BicicletaDTO
    {        
        public string Photo { get; set; }
        public BicicletaDTO()
        {
            this.Photo = "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";
        }
        public int BicicletaId { get; set; }

        [Range(0, 100,
        ErrorMessage = "Value for Weight must be between 0 and 100.")]
        public int Discount { get; set; }

        [MaxLength(100, ErrorMessage = "Maximum 100 characters allowed")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }


        [Range(0, 30,
        ErrorMessage = "Value for Weight must be between 0 and 30.")]
        [Required(ErrorMessage = "Weight is required")]
        public double Weight { get; set; }


        [Required(ErrorMessage = "Price is required")]
        [Range(0, 50000,
        ErrorMessage = "Value for Price must be between 0 and 50000.")]
        public double Price { get; set; }

        [MaxLength(50, ErrorMessage = "Maximum 50 characters allowed")]
        [Required(ErrorMessage = "Color is required")]
        public string Color { get; set; }



        [Required(ErrorMessage = "Issue year is required")]
        [Range(2000, 2030,
        ErrorMessage = "Value for Weight must be between 2000 and 2030.")]
        public int IssueYear { get; set; }

        [Range(0, 30,
        ErrorMessage = "Value for Weight must be between 0 and 30.")]
        [Required(ErrorMessage = "Wheel diameter is required")]
        public float WheelDiameter { get; set; }

        public int Quantity { get; set; }

        public StoreStatus QuantityStatus { get; set; }

        public BiciTypeDTO BiciType { get; set; }

        public int GetValueWithDiscount()
        {
            return (int)((Price * (100 - Discount)) / 100);
        }
    }
}
