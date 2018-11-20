using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClevelandTeaRevival.Models
{
    public class Tea
    {
        public int ID { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "First letter must be capitalized. No non-letter characters")]
        [StringLength(40, ErrorMessage = "Name cannot be longer than 40 characters.")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "First letter must be capitalized. No non-letter characters")]
        [StringLength(40, ErrorMessage = "Category cannot be longer than 40 characters.")]
        public string Category { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = "Description cannot be longer than 80 characters.")]
        public string Description { get; set; }

        public string ImageLink { get; set; }

        [DataType(DataType.Currency)]
        public decimal PricePerCup { get; set; }
        [DataType(DataType.Currency)]
        public decimal PricePerOz { get; set; }
        [DataType(DataType.Currency)]
        public decimal PricePerPot { get; set; }
        [DataType(DataType.Currency)]
        public decimal PricePerLb { get; set; }
        [DataType(DataType.Currency)]
        public decimal OtherPrice { get; set; }

        [NotMapped]
        [DataType(DataType.Currency)]
        public int Lbs { get; set; }

        [NotMapped]
        [DataType(DataType.Currency)]
        public int Ozs { get; set; }
    }
}
