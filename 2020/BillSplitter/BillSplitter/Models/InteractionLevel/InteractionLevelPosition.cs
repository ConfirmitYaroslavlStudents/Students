using System;
using System.ComponentModel.DataAnnotations;

namespace BillSplitter.Models.InteractionLevel
{
    public class InteractionLevelPosition
    {
        public int Id { get; set; }



        [Required(ErrorMessage = "Price is required")]
        [Range(typeof(decimal), "0.001", "79228162514264337593543950335", ErrorMessage = "Price should be greater than 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Range(1, 50, ErrorMessage = "The field Quantity must be between {1} and {2}.")]
        [Required(ErrorMessage = "Quantity is Required")]
        public int QuantityNumerator { get; set; } = 0;
        public int QuantityDenomenator { get; set; } = 1;
        public bool Selected { get; set; } = false;

        public Position ToPosition(int billId)
        {
            return new Position
            {
                Name = Name,
                Price = Price,
                Quantity = QuantityNumerator,
                BillId = billId
            };
        }
    }
}