using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BiciShop.Models
{
    public class Bicicleta
    {

        public string Photo { get; set; }
        public int BicicletaId { get; set; }

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

        [MaxLength(50, ErrorMessage ="Maximum 50 characters allowed")]
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

        public int BiciTypeId { get; set; }
        public BiciType BiciType { get; set; }
    }
}
